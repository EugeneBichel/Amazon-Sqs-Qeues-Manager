define(['ko'], function(ko) {
    'use strict';
    (function() {
        ko.observableArray.fn.setAtTop = function(value) {
            this.valueWillMutate();
            this().unshift(value);
            this.valueHasMutated();
        };
    })();
});