﻿(function ($) {
    'use strict';
    window.Rock = window.Rock || {};
    Rock.controls = Rock.controls || {};

    Rock.controls.connectionRequestBoard = (function () {

        let _controlClientId = '';

        //
        // Region: Loading Helpers
        //
        const showLoading = function (callback) {
            $('#connection-board-loading').show(callback);
        };

        const hideLoading = function () {
            $('#connection-board-loading').hide();
        };

        //
        // Region: URL Helpers
        //
        const getWorkflowCheckApiUrl = function (fromStatusId, toStatusId) {
            return Rock.settings.get('baseUrl') + 'api/ConnectionRequests/DoesStatusChangeCauseWorkflows/' + fromStatusId + '/' + toStatusId;
        };

        const getStatusViewModelsApiUrl = function (connectionOpportunityId) {
            return Rock.settings.get('baseUrl') + 'api/ConnectionRequests/ConnectionBoardStatusViewModels/' + connectionOpportunityId;
        };

        const getRequestViewModelsApiUrl = function (connectionRequestId) {
            return Rock.settings.get('baseUrl') + 'api/ConnectionRequests/ConnectionBoardRequestViewModel/' + connectionRequestId;
        };

        const getRequestDeleteApiUrl = function (connectionRequestId) {
            return Rock.settings.get('baseUrl') + 'api/ConnectionRequests/' + connectionRequestId;
        };

        //
        // Region: Confirmations
        //
        const promptConfirmation = function (message, okCallback, cancelCallback) {
            bootbox.dialog({
                message: message,
                buttons: {
                    ok: {
                        label: 'OK',
                        className: 'btn-primary',
                        callback: okCallback
                    },
                    cancel: {
                        label: 'Cancel',
                        className: 'btn-default',
                        callback: cancelCallback
                    }
                }
            });
        };

        //
        // Region: Templates and Rendering
        //
        const getColumnTemplate = function () {
            if (!_columnTemplate) {
                _columnTemplate = $('#template-column').html();
            }

            return _columnTemplate;
        };
        let _columnTemplate = '';

        const getColumnHtml = function (statusViewModel) {
            return resolveTemplateFields(getColumnTemplate(), statusViewModel);
        };

        const getCardTemplate = function () {
            if (!_cardTemplate) {
                _cardTemplate = $('#template-card').html();
            }

            return _cardTemplate;
        };
        let _cardTemplate = '';

        const getCardHtml = function (requestViewModel) {
            return resolveTemplateFields(getCardTemplate(), requestViewModel);
        };

        const getSentryTemplate = function () {
            if (!_sentryTemplate) {
                _sentryTemplate = $('#template-column-sentry').html();
            }

            return _sentryTemplate;
        };
        let _sentryTemplate = '';

        const resolveTemplateFields = function (template, viewModel) {
            const keys = Object.keys(viewModel);

            for (let i = 0; i < keys.length; i++) {
                const key = keys[i];
                template = template.replace(new RegExp('{{' + key + '}}', 'g'), viewModel[key]);
            }

            return template;
        };

        //
        // Region: API Callouts
        //
        const fetchStatusViewModels = function (options, callback) {
            const url = getStatusViewModelsApiUrl(options.connectionOpportunityId);

            const data = {
                sortProperty: options.sortProperty || 'Order',
                maxRequestsPerStatus: options.maxCardsPerColumn,
                statusIconsTemplate: options.statusIconsTemplate,
            };

            if (options.connectorPersonAliasId) {
                data.connectorPersonAliasId = options.connectorPersonAliasId;
            }

            if (options.minDate) {
                data.minDate = options.minDate;
            }

            if (options.campusId) {
                data.campusId = options.campusId;
            }

            if (options.maxDate) {
                data.maxDate = options.maxDate;
            }

            if (options.requesterPersonAliasId) {
                data.requesterPersonAliasId = options.requesterPersonAliasId
            }

            if (options.statusIds && options.statusIds.length) {
                data.delimitedStatusIds = options.statusIds.join('|')
            }

            if (options.connectionStates && options.connectionStates.length) {
                data.delimitedConnectionStates = options.connectionStates.join('|')
            }

            if (options.lastActivityTypeIds && options.lastActivityTypeIds.length) {
                data.delimitedLastActivityTypeIds = options.lastActivityTypeIds.join('|')
            }

            $.get({
                url: url,
                data: data,
                success: callback
            });
        };

        const fetchRequestViewModel = function (options, callback) {
            const url = getRequestViewModelsApiUrl(options.connectionRequestId);

            const data = {
                statusIconsTemplate: options.statusIconsTemplate
            };

            $.get({
                url: url,
                data: data,
                success: callback
            });
        };

        const deleteRequest = function (connectionRequestId, callback) {
            const url = getRequestDeleteApiUrl(connectionRequestId);

            $.ajax({
                url: url,
                type: 'DELETE',
                success: callback
            });
        };

        const doesStatusChangeCauseWorkflows = function (fromStatusId, toStatusId, callback) {
            if (fromStatusId === toStatusId) {
                callback(false);
                return;
            }

            const url = getWorkflowCheckApiUrl(fromStatusId, toStatusId);

            $.get({
                url: url,
                success: callback
            });
        }

        //
        // Region: DOM Manipulation
        //
        const fetchAndRefreshCard = function (options) {
            if (!options || !options.connectionRequestId) {
                return
            }

            fetchRequestViewModel(options, refreshCard);
        };

        const moveCard = function (connectionRequestId, newStatusId, newIndex) {
            const $newStatusCol = $('[data-status-id=' + newStatusId + ']');
            const $card = $('[data-request-id=' + connectionRequestId + ']');
            const $oldStatusCol = $card.closest('[data-status-id]');
            const oldStatusId = Number($oldStatusCol.attr('data-status-id'));

            $card.remove();

            const siblingIndex = newIndex - 1;
            const $sibling = siblingIndex ?
                $newStatusCol.find('[data-index=' + siblingIndex + ']') :
                null;

            if ($sibling && $sibling.length) {
                $sibling.after($card);
            }
            else {
                $newStatusCol.prepend($card);
            }

            indexCardsInColumn(newStatusId);
            indexCardsInColumn(oldStatusId);
        };

        const updateColCount = function (statusId, delta) {
            if (!delta) {
                return;
            }

            const $boardCountEl = $('[data-status-id=' + statusId + ']').closest('.board-column').find('.board-count');

            if (!$boardCountEl.length) {
                return;
            }

            const newCount = Number($boardCountEl.text()) + delta;
            $boardCountEl.text(newCount || 0);
        };

        const indexCardsInColumn = function (statusId) {
            $('[data-status-id=' + statusId + '] [data-request-id]').each(function (index, el) {
                const $card = $(el);
                $card.attr('data-index', index + 1);
            });
        };

        const fetchAndRenderStatusesAndCards = function (options) {
            showLoading(function () {
                fetchStatusViewModels(options, function (statusViewModels) {
                    renderStatusesAndCards(statusViewModels);
                    hideLoading();
                });
            });
        };

        const renderStatusesAndCards = function (statusViewModels) {
            if (!statusViewModels || !Array.isArray(statusViewModels)) {
                return;
            }

            const $columnContainer = $('.js-column-container');
            $columnContainer.empty();

            for (let i = 0; i < statusViewModels.length; i++) {
                const statusViewModel = statusViewModels[i];
                const $column = $(getColumnHtml(statusViewModel));
                const $cardContainer = $column.find('.js-card-container');

                for (let j = 0; j < statusViewModel.Requests.length; j++) {
                    const requestViewModel = statusViewModel.Requests[j];
                    const requestCardHtml = getCardHtml(requestViewModel);
                    $cardContainer.append(requestCardHtml);
                }

                if (statusViewModel.Requests.length < statusViewModel.RequestCount) {
                    $cardContainer.append(getSentryTemplate());
                }

                $columnContainer.append($column);
                indexCardsInColumn(statusViewModel.Id);
            }

            $('[data-toggle=tooltip]').tooltip();
            initializeDraggingCards();
        };

        const refreshCard = function (requestViewModel) {
            if (!requestViewModel) {
                return;
            }

            const $oldCard = $('.board-card[data-request-id=' + requestViewModel.Id + ']');
            const newCardHtml = getCardHtml(requestViewModel);
            $oldCard.replaceWith(newCardHtml);

            indexCardsInColumn(requestViewModel.StatusId);
        };

        const removeCard = function (connectionRequestId) {
            $('.board-card[data-request-id=' + connectionRequestId + ']').remove();
        };

        const deleteRequestAndRemoveCard = function (connectionRequestId) {
            promptConfirmation('Are you sure you want to delete this request?', function () {
                deleteRequest(connectionRequestId, function () {
                    removeCard(connectionRequestId);
                });
            });
        };

        //
        // Region: Events
        //
        const oneTimeInitializeEvents = function () {
            if (_areOneTimeEventsInitialized) {
                return;
            }

            _areOneTimeEventsInitialized = true;

            // Allow clicking on card to view
            $('.js-column-container').on('click', '.board-card-content', onCardClick);

            // Initialize dropdown buttons
            $('.btn-group-mega').each(function () {
                const $self = $(this);
                const bottom = $self.children('.dropdown-toggle').first().position().top + $self.height();
                $self.children('.dropdown-menu-mega').first().css('top', bottom);
            });
        };
        let _areOneTimeEventsInitialized = false;

        const initializeDraggingCards = function () {
            var drake = dragula($('.board-cards, .drag-scroll-zone').get(), {
                revertOnSpill: true,
                moves: function (el) {
                    return $(el).hasClass('board-card');
                }
            });

            drake.on('drop', onCardDrop);
            drake.on('over', onCardOver);
            drake.on('out', onCardOut);
        };

        const onCardOver = function (el, container) {
            // el (card) is being dragged (not yet dropped) over container (col)
            const $container = $(container);

            if (!$container.hasClass('drag-scroll-zone')) {
                return;
            }

            let scrollY = 0;
            let scrollX = 0;

            if ($container.hasClass('drag-scroll-zone-left')) {
                scrollX = -10;
            }
            else if ($container.hasClass('drag-scroll-zone-right')) {
                scrollX = 10;
            }
            else if ($container.hasClass('drag-scroll-zone-top')) {
                scrollY = -5;
            }
            else if ($container.hasClass('drag-scroll-zone-bottom')) {
                scrollY = 5;
            }
            else {
                return;
            }

            clearInterval(_timer);
            _timer = setInterval(function () {
                if (scrollX) {
                    const $dragscroll = $('.dragscroll');
                    $dragscroll.scrollLeft($dragscroll.scrollLeft() + scrollX);
                }
                else if (scrollY) {
                    const $scrollElement = $container.siblings('.js-card-container');
                    $scrollElement.scrollTop($scrollElement.scrollTop() + scrollY);
                }
            }, 15);
        };
        let _timer = null;

        const onCardOut = function (el, container) {
            // A card (el) was being dragged over col (container) but has now left
            clearInterval(_timer);
        };

        const onCardDrop = function (el, target, source) {
            // el (card) was dropped into target (col), and originally came from source (col)

            const $target = $(target);
            const $source = $(source);
            const $el = $(el);

            const newStatusId = $target.data('statusId');
            const oldStatusId = $source.data('statusId');
            const newIndex = $target.children().index(el);
            const originalIndex = Number($el.attr('data-index'));
            const requestId = $el.data('requestId');

            if ($target.hasClass('drag-scroll-zone')) {
                // Put the card back out of the scroll zone
                moveCard(requestId, oldStatusId, originalIndex);
                return;
            }

            doesStatusChangeCauseWorkflows(oldStatusId, newStatusId, function (data) {
                if (data && data.DoesCauseWorkflows) {
                    const from = data.FromStatusName ? data.FromStatusName : 'that status';
                    const to = data.ToStatusName ? data.ToStatusName : 'this status';
                    const msg = 'Changing the status from ' + from + ' to ' + to + 'will trigger workflows to launch. Do you wish to continue with this change ?';

                    promptConfirmation(msg,
                        function () {
                            sendPostback('card-drop-confirmed', requestId, newStatusId, newIndex);

                            updateColCount(newStatusId, 1);
                            indexCardsInColumn(newStatusId);

                            updateColCount(oldStatusId, -1);
                            indexCardsInColumn(oldStatusId);
                        },
                        function () {
                            moveCard(requestId, oldStatusId, originalIndex);
                        }
                    );
                }
                else {
                    sendPostback('card-drop-confirmed', requestId, newStatusId, newIndex);

                    updateColCount(newStatusId, 1);
                    indexCardsInColumn(newStatusId);

                    updateColCount(oldStatusId, -1);
                    indexCardsInColumn(oldStatusId);
                }
            });
        };

        const onCardClick = function (event) {
            const $target = $(event.target);
            const connectionRequestId = $target.closest('[data-request-id]').data('requestId');

            if (!connectionRequestId) {
                return;
            }

            if ($target.closest('.js-delete').length) {
                deleteRequestAndRemoveCard(connectionRequestId);
                return;
            }

            if ($target.closest('.js-view').length) {
                sendPostback('view', connectionRequestId);
                return;
            }

            if ($target.closest('.js-connect').length) {
                sendPostback('connect', connectionRequestId);
                return;
            }

            if ($target.closest('button').length || $target.closest('a').length) {
                // Ignore clicks on other buttons and anchors on the card
                return;
            }

            // If the execution makes it this far then the user clicked the card (not a button) and we will open the modal
            sendPostback('view', connectionRequestId);
        };

        //
        // Region: Postback
        //
        const sendPostback = function (/* ... arguments */) {
            if (!_controlClientId) {
                return;
            }

            const argsString = Array.prototype.slice.call(arguments).join('|');
            const postback = 'javascript:__doPostBack(' + JSON.stringify(_controlClientId) + ', ' + JSON.stringify(argsString) + ')';
            window.location = postback;
        };

        //
        // Region: Init Method
        //

        /**
         * Initializes the board by fetching and rendering cards. Duplicate work is prevented
         * by tracking the last options object and comparing it to the new options object.
         * @param {any} options
         */
        const initialize = function (options) {
            if (!options || !options.connectionOpportunityId || !options.controlClientId) {
                throw 'A valid options object is required';
            }

            // Check if this options object has already been initialized (no need to do it again)
            const newOptionsHash = JSON.stringify(options);
            const areColsRendered = $('.js-column-container > *').length > 0;

            if (newOptionsHash === _optionsHash && areColsRendered) {
                return;
            }

            // Initialize the board by rendering the cols and cards
            _optionsHash = newOptionsHash;
            _controlClientId = options.controlClientId;
            fetchAndRenderStatusesAndCards(options);

            // Reset dragscrollnow that the board is refreshed
            dragscroll.reset();

            oneTimeInitializeEvents();
        };
        let _optionsHash = '';

        // Return exposed functions
        return {
            initialize: initialize,
            fetchAndRefreshCard: fetchAndRefreshCard,
        };

    })();
}(jQuery));
