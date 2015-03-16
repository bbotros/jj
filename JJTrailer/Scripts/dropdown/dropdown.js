/*
 * jQuery dropdown2: A simple dropdown2 plugin
 *
 * Copyright A Beautiful Site, LLC. (http://www.abeautifulsite.net/)
 *
 * Licensed under the MIT license: http://opensource.org/licenses/MIT
 *
*/
if (jQuery) (function ($) {

    $.extend($.fn, {
        dropdown2: function (method, data) {

            switch (method) {
                case 'show':
                    show(null, $(this));
                    return $(this);
                case 'hide':
                    hide();
                    return $(this);
                case 'attach':
                    return $(this).attr('data-dropdown2', data);
                case 'detach':
                    hide();
                    return $(this).removeAttr('data-dropdown2');
                case 'disable':
                    return $(this).addClass('dropdown2-disabled');
                case 'enable':
                    hide();
                    return $(this).removeClass('dropdown2-disabled');
            }

        }
    });

    function show(event, object) {

        var trigger = event ? $(this) : object,
			dropdown2 = $(trigger.attr('data-dropdown2')),
			isOpen = trigger.hasClass('dropdown2-open');

        // In some cases we don't want to show it
        if (event) {
            if ($(event.target).hasClass('dropdown2-ignore')) return;

            event.preventDefault();
            event.stopPropagation();
        } else {
            if (trigger !== object.target && $(object.target).hasClass('dropdown2-ignore')) return;
        }
        hide();

        if (isOpen || trigger.hasClass('dropdown2-disabled')) return;

        // Show it
        trigger.addClass('dropdown2-open');
        dropdown2
			.data('dropdown2-trigger', trigger)
			.show();

        // Position it
        position();

        // Trigger the show callback
        dropdown2
			.trigger('show', {
			    dropdown2: dropdown2,
			    trigger: trigger
			});

    }

    function hide(event) {

        // In some cases we don't hide them
        var targetGroup = event ? $(event.target).parents().addBack() : null;

        // Are we clicking anywhere in a dropdown2?
        if (targetGroup && targetGroup.is('.dropdown2')) {
            // Is it a dropdown2 menu?
            if (targetGroup.is('.dropdown2-menu')) {
                // Did we click on an option? If so close it.
                if (!targetGroup.is('A')) return;
            } else {
                // Nope, it's a panel. Leave it open.
                return;
            }
        }

        // Hide any dropdown2 that may be showing
        $(document).find('.dropdown2:visible').each(function () {
            var dropdown2 = $(this);
            dropdown2
				.hide()
				.removeData('dropdown2-trigger')
				.trigger('hide', { dropdown2: dropdown2 });
        });

        // Remove all dropdown2-open classes
        $(document).find('.dropdown2-open').removeClass('dropdown2-open');

    }

    function position() {

        var dropdown2 = $('.dropdown2:visible').eq(0),
			trigger = dropdown2.data('dropdown2-trigger'),
			hOffset = trigger ? parseInt(trigger.attr('data-horizontal-offset') || 0, 10) : null,
			vOffset = trigger ? parseInt(trigger.attr('data-vertical-offset') || 0, 10) : null;

        if (dropdown2.length === 0 || !trigger) return;

        // Position the dropdown2 relative-to-parent...
        if (dropdown2.hasClass('dropdown2-relative')) {
            dropdown2.css({
                left: dropdown2.hasClass('dropdown2-anchor-right') ?
					trigger.position().left - (dropdown2.outerWidth(true) - trigger.outerWidth(true)) - parseInt(trigger.css('margin-right'), 10) + hOffset :
					trigger.position().left + parseInt(trigger.css('margin-left'), 10) + hOffset,
                top: trigger.position().top + trigger.outerHeight(true) - parseInt(trigger.css('margin-top'), 10) + vOffset
            });
        } else {
            // ...or relative to document
            dropdown2.css({
                left: dropdown2.hasClass('dropdown2-anchor-right') ?
					trigger.offset().left - (dropdown2.outerWidth() - trigger.outerWidth()) + hOffset : trigger.offset().left + hOffset,
                top: trigger.offset().top + trigger.outerHeight() + vOffset
            });
        }
    }

    $(document).on('click.dropdown2', '[data-dropdown2]', show);
    $(document).on('click.dropdown2', hide);
    $(window).on('resize', position);

})(jQuery);