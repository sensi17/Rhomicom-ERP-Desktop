/*
VERSION: 3.2
DATE: 3/23/2007
DESCRIPTION: 
	 This class provides an easy way to load and/or send an XML file and parse the data into a format that's simple 
	 to work with. Every node becomes an array with the same name. All attributes are also easily accessible because 
	 they become properties with the same name. So for example, if this is your XML:
	 
		<Resources>
			<Book name="Mary Poppins" ISDN="1122563" />
			<Book name="The Bible" ISDN="333777" />
			<Novel name="The Screwtape Letters" ISDN="257896">
				<Description>This is an interesting perspective</Description>
			</Novel>
		</Resources>
	 
	 Then you could access the first book's ISDN with:
	 
	 	Book[0].ISDN
	 
	 The value of a node (like the text between the <Description> and </Description> tags above can
	 be accessed using the "value" property, like so:
	 
	 	Novel[0].Description[0].value
	 
	 Just remember that all nodes become arrays even if there's only one node, and attributes become properties. 
	 You can obviously loop through the arrays too which is very useful. The root node is ignored for efficiency 
	 (less code for you to write).

EXAMPLE: 
	To simply load a "myDocument.xml" document and parse the data into ActionScript-friendly values, do:
	
		import gs.dataTransfer.XMLParser;
		var parsed_obj = new Object(); //This will hold the parsed xml data (once the XML loads and gets parsed).
		XMLParser.load("myDocument.xml", onFinish, parsed_obj);
		function onFinish(success_boolean, results_obj, xml) { //This function gets called when the XML gets parsed.
			if (success_boolean) {
				trace("The first book is: "+results_obj.Book[0].name);
			}
		}
		
	Or to send an object to the server in XML format (remember, each element in an array becomes a node and all 
	object properties become node attributes) and load the results back into an ActionScript-friendly format, do:
	
		import gs.dataTransfer.XMLParser;
		//Create an object to send an populate it with values...
		var toSend_obj = new Object();
		toSend_obj.name = "Test Name";
		toSend_obj.Book = new Array();
		toSend_obj.Book.push({title:"Mary Poppins", ISDN:"125486523"});
		toSend_obj.Book.push({title:"The Bible", ISDN:"25478866998"});
		//Now send the data and load the results from the server into the response_obj...
		var response_obj = new Object(); //We'll use this to hold the parsed xml response.
		XMLParser.sendAndLoad(toSend_obj, "http://www.myDomain.com/myScript.php", onFinish, response_obj);
		function onFinish(success_boolean, results_obj, xml) {
			if (success_boolean) {
				trace("The server responded with this XML: "+xml);
				trace("The server's response was translated into this ActionScript object: "+results_obj);
			}
		}
		
		In the example above, the server would receive the following XML document:
		
		<XML name="Test Name">
			<Book ISDN="125486523" title="Mary Poppins" />
			<Book ISDN="25478866998" title="The Bible" />
		</XML>
	
NOTES:
	- It is case sensitive, so if you run into problems, check that.
	- The value of any text node can be accessed with the "value" property as mentioned above.
	- A valid XML document requires a single root element, so in order to consolidate things,
	  That root will be ignored in the resulting arrays. So if your root element is <Library>
	  and it has <Book> nodes, you don't have to access them with Library[0].Book[0]. You can 
	  just do Book[0].
	- You can simply translate an object into XML (without sending it anywhere) using the 
	  XMLParser.objectToXML(my_obj) function which returns an XML instance.
		
CODED BY: Jack Doyle, jack@greensock.com
*/
import mx.utils.Delegate;

class gs.dataTransfer.XMLParser {
	static var CLASS_REF = gs.dataTransfer.XMLParser;
	private static var _parsers_array:Array;
	private var _url_str:String;
	private var _onComplete_func:Function;
	private var _xml:XML;
	private var _results_obj:Object;
	var parse:Function; //Just for backward compatibility. It's essentially an alias pointing to the initLoad() function.
	
	function XMLParser() {
		parse = initLoad; //Just for backward compatibility. It's essentially an alias pointing to the initLoad() function.
		if (_parsers_array == undefined) {
			_parsers_array = [];
		}
		_parsers_array.push(this);
	}
	
	static function load(url_str:String, onComplete_func:Function, results_obj:Object):XMLParser {
		var parser_obj = new XMLParser();
		parser_obj.initLoad(url_str, onComplete_func, results_obj);
		return parser_obj;
	}
	
	static function sendAndLoad(toSend_obj:Object, url_str:String, onComplete_func:Function, results_obj:Object):XMLParser {
		var parser_obj = new XMLParser();
		parser_obj.initSendAndLoad(toSend_obj, url_str, onComplete_func, results_obj);
		return parser_obj;
	}
	
	
	function initLoad(url_str:String, onComplete_func:Function, results_obj:Object) {
		if (results_obj == undefined) {
			results_obj = {};
		}
		_results_obj = results_obj;
		_url_str = url_str;
		_onComplete_func = onComplete_func;
		_xml = new XML();
		_xml.ignoreWhite = true;
		_xml.onLoad = Delegate.create(this, this.parseLoadedXML);
		_xml.load(_url_str);
	}
	
	function initSendAndLoad(toSend_obj:Object, url_str:String, onComplete_func:Function, results_obj:Object) {
		if (results_obj == undefined) {
			results_obj = {};
		}
		_results_obj = results_obj;
		_url_str = url_str;
		_onComplete_func = onComplete_func;
		if (toSend_obj instanceof XML) {
			var xmlToSend_obj = toSend_obj;
		} else {
			var xmlToSend_obj = XMLParser.objectToXML(toSend_obj);
		}
		_xml = new XML();
		_xml.ignoreWhite = true;
		_xml.onLoad = Delegate.create(this, this.parseLoadedXML);
		xmlToSend_obj.sendAndLoad(_url_str, _xml);
	}
	
	
	function searchAndReplace(holder, searchfor, replacement) {
	var temparray = holder.split(searchfor);
	var holder = temparray.join(replacement);
	return (holder);
	}
	
	
	private function parseLoadedXML(success_boolean) {
		if (success_boolean == false) {
			trace("XML FAILED TO LOAD! ("+_url_str+")");
			_onComplete_func(false);
			return;
		}
		var x = this._xml;
		var c = x.firstChild.firstChild; //"c" is for current_node
		var last_node = x.firstChild.lastChild;
		x.firstChild.obj = _results_obj; //Allows us to tack on all the arrays and objects to this instance for easy retrieval by the user. If this causes a problem, we could create a public object variable that holds everything, but this simplifies things for the user.
		while(c != undefined) {
			//We ran into an issue where Flash was creating an extra subnode anytime we had content in a node like <NODE>My Content</NODE>. The tip off is when the nodeName is null and the nodeType is 3 (text).
			if (c.nodeName == null && c.nodeType == 3) {
				c.parentNode.obj.value = searchAndReplace(c.nodeValue, '\r\n', '');
			} else {
				var o = {};
				for (var att in c.attributes) {
					o[att] = c.attributes[att];
				}
				var pn = c.parentNode.obj;
				if (pn[c.nodeName] == undefined) {
					pn[c.nodeName] = [];
				}
				c.obj = o;
				pn[c.nodeName].push(o);
			}
			
			if (c.childNodes.length > 0) {
				c = c.childNodes[0];
			} else {
				var next_node = c;
				while(next_node.nextSibling == undefined && next_node.parentNode != undefined) {
					next_node = next_node.parentNode;
				}
				c = next_node.nextSibling;
				if (next_node == last_node) {
					c = undefined;
				}
			}
		}
		_onComplete_func(true, _results_obj, x);
	}
	
	//Allows us to translate an object (typically with arrays attached to it) back into an XML object. This is useful when we need to send it back to the server or save it somewhere.
	public static function objectToXML(o:Object, rootNodeName_str:String):XML {
		if (rootNodeName_str == undefined) {
			rootNodeName_str = "XML";
		}
		var xml:XML = new XML();
		var n:XMLNode = xml.createElement(rootNodeName_str);
		var props = [];
		var prop;
		for (var p in o) {
			props.push(p);
		}
		for (var p = props.length - 1; p >= 0; p--) { //By default, attributes are looped through in reverse, so we go the opposite way to accommodate for this.
			prop = props[p];
			if (typeof(o[prop]) == "object" && o[prop].length > 0) { //Means it's an array!
				arrayToNodes(o[prop], n, xml, prop);
			} else if (prop == "value") {
				var tn:XMLNode = xml.createTextNode(o.value);
				n.appendChild(tn);
			} else {
				n.attributes[prop] = o[prop];
			}
		}
		xml.appendChild(n);
		return xml;
	}
	
	//Recursive function that walks through any sub-arrays as well...
	private static function arrayToNodes(ar:Array, parentNode:XMLNode, xml:XML, nodeName_str:String):Void {
		var chldrn = [];
		var props:Array;
		var prop;
		var n:XMLNode;
		var o:Object;
		for (var i = ar.length - 1; i >= 0; i--) {
			n = xml.createElement(nodeName_str);
			o = ar[i];
			props = [];
			for (var p in o) {
				props.push(p);
			}
			for (var p = props.length - 1; p >= 0; p--) { //By default, attributes are looped through in reverse, so we go the opposite way to accommodate for this.
				prop = props[p];
				if (typeof(o[prop]) == "object" && o[prop].length > 0) { //Means it's an array!
					arrayToNodes(o[prop], n, xml, prop);
				} else if (prop != "value") {
					n.attributes[prop] = o[prop];
				} else {
					var tn:XMLNode = xml.createTextNode(o.value);
					n.appendChild(tn);
				}
			}
			chldrn.push(n);
			//parentNode.appendChild(n);
		}
		for (var i = chldrn.length - 1; i >= 0; i--) {
			parentNode.appendChild(chldrn[i]);
		}
	}
	
	public function destroy():Void {
		delete _xml;
		for (var i = 0; i < _parsers_array.length; i++) {
			if (this == _parsers_array[i]) {
				_parsers_array.splice(i, 1);
			}
		}
		destroyInstance(this);
	}
	
	static function destroyInstance(i:XMLParser):Void {
		delete i;
	}
	
//---- GETTERS / SETTERS --------------------------------------------------------------------
	static function get active_boolean():Boolean {
		if (_parsers_array.length > 0) {
			return true;
		} else {
			return false;
		}
	}
	
}