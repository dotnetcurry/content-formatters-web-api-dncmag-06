/// <reference path="knockout-2.2.0.js" />
// Source Ryan Niemeyer's JS Fiddle at http://jsfiddle.net/rniemeyer/X9rRa/
//wrapper for an observable that protects value until committed
ko.protectedObservable = function (initialValue)
{
    //private variables
    var _temp = initialValue;
    var _actual = ko.observable(initialValue);
    var result = ko.dependentObservable({
        read: _actual,
        write: function (newValue)
        {
            _temp = newValue;
        }
    });

    //commit the temporary value to our observable, if it is different
    result.commit = function ()
    {
        if (_temp !== _actual())
        {
            _actual(_temp);
        }
    };

    //notify subscribers to update their value with the original
    result.reset = function ()
    {
        _actual.valueHasMutated();
        _temp = _actual();
    };
    return result;
};
