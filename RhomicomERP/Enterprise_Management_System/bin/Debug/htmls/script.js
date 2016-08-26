/* begin Page */
/* Created by Artisteer v3.0.0.45570 */
// css hacks
(function($) {
    // fix ie blinking
    var m = document.uniqueID && document.compatMode && !window.XMLHttpRequest && document.execCommand;
    try { if (!!m) { m('BackgroundImageCache', false, true); } }
    catch (oh) { };
    // css helper
    var data = [
        {str:navigator.userAgent,sub:'Chrome',ver:'Chrome',name:'chrome'},
        {str:navigator.vendor,sub:'Apple',ver:'Version',name:'safari'},
        {prop:window.opera,ver:'Opera',name:'opera'},
        {str:navigator.userAgent,sub:'Firefox',ver:'Firefox',name:'firefox'},
        {str:navigator.userAgent,sub:'MSIE',ver:'MSIE',name:'ie'}];
    for (var n=0;n<data.length;n++)	{
        if ((data[n].str && (data[n].str.indexOf(data[n].sub) != -1)) || data[n].prop) {
            var v = function(s){var i=s.indexOf(data[n].ver);return (i!=-1)?parseInt(s.substring(i+data[n].ver.length+1)):'';};
            $('html').addClass(data[n].name+' '+data[n].name+v(navigator.userAgent) || v(navigator.appVersion)); break;			
        }
    }
})(jQuery);

var _artStyleUrlCached = null;
function artGetStyleUrl() {
    if (null == _artStyleUrlCached) {
        var ns;
        _artStyleUrlCached = '';
        ns = jQuery('link');
        for (var i = 0; i < ns.length; i++) {
            var l = ns[i].href;
            if (l && /style\.ie6\.css(\?.*)?$/.test(l))
                return _artStyleUrlCached = l.replace(/style\.ie6\.css(\?.*)?$/, '');
        }
        ns = jQuery('style');
        for (var i = 0; i < ns.length; i++) {
            var matches = new RegExp('import\\s+"([^"]+\\/)style\\.ie6\\.css"').exec(ns[i].html());
            if (null != matches && matches.length > 0)
                return _artStyleUrlCached = matches[1];
        }
    }
    return _artStyleUrlCached;
}

function artFixPNG(element) {
    if (jQuery.browser.msie && parseInt(jQuery.browser.version) < 7) {
		var src;
		if (element.tagName == 'IMG') {
			if (/\.png$/.test(element.src)) {
				src = element.src;
				element.src = artGetStyleUrl() + 'images/spacer.gif';
			}
		}
		else {
			src = element.currentStyle.backgroundImage.match(/url\("(.+\.png)"\)/i);
			if (src) {
				src = src[1];
				element.runtimeStyle.backgroundImage = 'none';
			}
		}
		if (src) element.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "')";
	}
}

jQuery(function() {
    jQuery.each(jQuery('ul.rho-hmenu>li:not(.rho-hmenu-li-separator),ul.rho-vmenu>li:not(.rho-vmenu-separator)'), function (i, val) {
        var l = jQuery(val); var s = l.children('span'); if (s.length == 0) return;
        var t = l.find('span.t').last(); l.children('a').append(t.html(t.text()));
        s.remove();
    });
});/* end Page */

/* begin Box, Sheet */

function artFluidSheetComputedWidth(percent, minval, maxval) {
    percent = parseInt(percent);
    var val = document.body.clientWidth / 100 * percent;
    return val < minval ? minval + 'px' : val > maxval ? maxval + 'px' : percent + '%';
}/* end Box, Sheet */

/* begin Menu */
jQuery(function() {
    jQuery.each(jQuery('ul.rho-hmenu>li:not(:last-child)'), function(i, val) {
        jQuery('<li class="rho-hmenu-li-separator"><span class="rho-hmenu-separator"> </span></li>').insertAfter(val);
    });
    if (!jQuery.browser.msie || parseInt(jQuery.browser.version) > 6) return;
    jQuery.each(jQuery('ul.rho-hmenu li'), function(i, val) {
        val.j = jQuery(val);
        val.UL = val.j.children('ul:first');
        if (val.UL.length == 0) return;
        val.A = val.j.children('a:first');
        this.onmouseenter = function() {
            this.j.addClass('rho-hmenuhover');
            this.UL.addClass('rho-hmenuhoverUL');
            this.A.addClass('rho-hmenuhoverA');
        };
        this.onmouseleave = function() {
            this.j.removeClass('rho-hmenuhover');
            this.UL.removeClass('rho-hmenuhoverUL');
            this.A.removeClass('rho-hmenuhoverA');
        };

    });
});

/* end Menu */

/* begin Layout */
jQuery(function () {
     var c = jQuery('div.rho-content');
    if (c.length !== 1) return;
    var s = c.parent().children('.rho-layout-cell:not(.rho-content)');
    jQuery(window).bind('resize', function () {
        c.css('height', 'auto');
        var r = jQuery(window).height() - jQuery('#rho-main').height();
        if (r > 0) c.css('height', r + c.height() + 'px');
    });

    if (jQuery.browser.msie && parseInt(jQuery.browser.version) < 8) {

        jQuery(window).bind('resize', function () {
            var w = 0;
            c.hide();
            s.each(function () { w += this.clientWidth; });
            c.w = c.parent().width(); c.css('width', c.w - w + 'px');
            c.show();
        })

        var r = jQuery('div.rho-content-layout-row').each(function () {
            this.c = jQuery(this).children('.rho-layout-cell:not(.rho-content)');
        });

        jQuery(window).bind('resize', function () {
            r.each(function () {
                if (this.h == this.clientHeight) return;
                this.c.css('height', 'auto');
                var r = jQuery(window).height() - jQuery('#rho-main').height();
                this.h = this.clientHeight;
                if (r > 0) this.h += r;
                this.c.css('height', this.h + 'px');
            });
        });
    }

    var g = jQuery('.rho-layout-glare-image');
    jQuery(window).bind('resize', function () {
        g.each(function () {
            var i = jQuery(this);
            i.css('height', i.parents('.rho-layout-cell').height() + 'px');
        });
    });

    jQuery(window).trigger('resize');
});/* end Layout */

/* begin VMenu */
jQuery(function() {
    jQuery('ul.rho-vmenu li').not(':first').before('<li class="rho-vsubmenu-separator"><span class="rho-vsubmenu-separator-span"> </span></li>');
    jQuery('ul.rho-vmenu > li.rho-vsubmenu-separator').removeClass('rho-vsubmenu-separator').addClass('rho-vmenu-separator').children('span').removeClass('rho-vsubmenu-separator-span').addClass('rho-vmenu-separator-span');
    jQuery('ul.rho-vmenu > li > ul > li.rho-vsubmenu-separator:first-child').removeClass('rho-vsubmenu-separator').addClass('rho-vmenu-separator').addClass('rho-vmenu-separator-first').children('span').removeClass('rho-vsubmenu-separator-span').addClass('rho-vmenu-separator-span');
});  /* end VMenu */

/* begin VMenuItem */
jQuery(function() {
    jQuery('ul.rho-vmenu a').click(function () {
        var a = jQuery(this);
        a.parents('ul.rho-vmenu').find("ul, a").removeClass('active');
        a.parent().children('ul').addClass('active');
        a.parents('ul.rho-vmenu ul').addClass('active');
        a.parents('ul.rho-vmenu li').children('a').addClass('active');
    });
});
/* end VMenuItem */

/* begin Button */
function artButtonSetup(className) {
    jQuery.each(jQuery("a." + className + ", button." + className + ", input." + className), function (i, val) {
        var b = jQuery(val);
        if (!b.parent().hasClass('rho-button-wrapper')) {
            if (b.is('input')) b.val(b.val().replace(/^\s*/, '')).css('zoom', '1');
            if (!b.hasClass('rho-button')) b.addClass('rho-button');
            jQuery("<span class='rho-button-wrapper'><span class='rho-button-l'> </span><span class='rho-button-r'> </span></span>").insertBefore(b).append(b);
            if (b.hasClass('active')) b.parent().addClass('active');
        }
        b.mouseover(function () { jQuery(this).parent().addClass("hover"); });
        b.mouseout(function () { var b = jQuery(this); b.parent().removeClass("hover"); if (!b.hasClass('active')) b.parent().removeClass('active'); });
        b.mousedown(function () { var b = jQuery(this); b.parent().removeClass("hover"); if (!b.hasClass('active')) b.parent().addClass('active'); });
        b.mouseup(function () { var b = jQuery(this); if (!b.hasClass('active')) b.parent().removeClass('active'); });
    });
}
jQuery(function() { artButtonSetup("rho-button"); });

/* end Button */



