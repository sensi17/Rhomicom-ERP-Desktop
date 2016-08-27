import flash.filters.ColorMatrixFilter;

var mainObj = _root.parsed_obj;
var defaultAutoSize = true;

/*

Examples:

_root.getConfigOption( 'websiteURL' );

var format:TextFormat = new TextFormat();
format.letterSpacing = 0;
_root.getSystemText( thisText, 0, "center", format );

var format:TextFormat = new TextFormat();
format.letterSpacing = 0;
_root.getSloganText( thisText, 0, 0, "right", format );
_root.getSloganText( thisText, 0, 1, "left", format );
_root.getSloganText( thisText, 2, 1, "center", format );

_root.getImage( 0 );
_root.getImage( 4 );

*/

// Constants
var SECTION_TAG_NAME:String = 'section';
var CONFIG_SECTION_NAME:String = 'configuration';
var SLOGANS_SECTION_NAME:String = 'slogans';
var TEXTS_SECTION_NAME:String = 'systemTexts';
var IMAGES_SECTION_NAME:String = 'images';
var MUSIC_SECTION_NAME:String = 'music';

function getXmlSection(obj, sectionName):Number
{
	var i:Number=0;
	while ( obj[SECTION_TAG_NAME][i] ) {
		if ( obj[SECTION_TAG_NAME][i].name==sectionName ) {
			return i;
		}
		i++;
	}
}

function getSystemText(textObj, textNumber, autoSizeStyle, format):Void
{
	var sectionNum:Number = getXmlSection( mainObj, TEXTS_SECTION_NAME );
	textObj.htmlText = mainObj[SECTION_TAG_NAME][sectionNum]["text"][textNumber].value;
	if( format ) textObj.setTextFormat( format );
	updateAutoSizeStyle( textObj, autoSizeStyle );
}

function setColorMatrix(target):Void
{
	var matrix:Array = [];
	matrix = String(getConfigOption('matrix')).split(',');
	if(matrix.length == 20)
	{
		var colorMatrixFilter:ColorMatrixFilter = new ColorMatrixFilter(matrix);
		target.filters = [colorMatrixFilter];
	}
}

function getImage(imageNumber):String
{
	if(imageNumber>=0) {
		var sectionNum:Number = getXmlSection( mainObj, IMAGES_SECTION_NAME );
		return mainObj[SECTION_TAG_NAME][sectionNum]["image"][imageNumber]["url"];
	} else {
		return '';
	}
}

function getMusic(musicNumber):String
{
	if(musicNumber>=0) {
		var sectionNum:Number = getXmlSection( mainObj, MUSIC_SECTION_NAME );
		return mainObj[SECTION_TAG_NAME][sectionNum]["music"][musicNumber]["url"];
	} else {
		return '';
	}
}

function getSloganText(textObj, sloganNumber, textNumber, autoSizeStyle, format):Void
{
	var sectionNum:Number = getXmlSection( mainObj, SLOGANS_SECTION_NAME );
	var sloganEnabled:Boolean = (mainObj[SECTION_TAG_NAME][sectionNum]["slogan"][sloganNumber]['enabled'] == "true") ? true : false;
	if(sloganEnabled) {
		textObj.htmlText = mainObj[SECTION_TAG_NAME][sectionNum]["slogan"][sloganNumber]["text"][textNumber].value;
		if( format ) textObj.setTextFormat( format );
		updateAutoSizeStyle( textObj, autoSizeStyle );
	} else {
		textObj.htmlText = '';
	}
}

function getConfigOption(optionName):String
{
	var sectionNum:Number = getXmlSection( mainObj, CONFIG_SECTION_NAME );
	return mainObj[SECTION_TAG_NAME][sectionNum][optionName][0].value;
}

function updateAutoSizeStyle(textObj, autoSizeStyle):Void
{
	if( autoSizeStyle == true || autoSizeStyle == 'left' || autoSizeStyle == false ||
		autoSizeStyle == 'right' || autoSizeStyle == 'center' ) {
		textObj.autoSize = autoSizeStyle;
	} else {
		textObj.autoSize = defaultAutoSize;
	}
}