/// <reference path="_references.js" />

$(document).ready(function ()
{
    $.ajax({
        url: "/api/TimeCard",
        method: "GET",
        contentType: "text/json",
        success: function (data)
        {
            viewModel.timecards.removeAll();
            $.each(data, function (index)
            {
                viewModel.timecards.push(addNewTimeCard(data[index]));
            });
            ko.applyBindings(viewModel);
        }
    });

    $(document).delegate("#submitButton", "click", function ()
    {
        viewModel.commitAll();
        var vm = viewModel.selectedTimeCard;
        var current = ko.utils.unwrapObservable(vm);
        var stringyF = JSON.stringify(ko.mapping.toJS(current));
        var action = "PUT";
        var vUrl = "/api/TimeCard?Id=" + current.Id();
        if (current.Id()==0)
        {
            action = "POST";
            vUrl = "/api/TimeCard";
        }
        $.ajax(
        {
            url: vUrl,
            contentType: "application/json;charset=utf-8",
            type: action,
            data: stringyF,
            success: function (response)
            {
                alert("Saved Successfully");
                viewModel.selectedTimeCard(null);
            },
            failure: function (response)
            {
                alert("Save Failed");
            }
        });
    });

    $(document).delegate("#cancelButton", "click", function ()
    {
        viewModel.selectedTimeCard(null);
    });

    $(document).delegate(".downloadButton", "click", function()
    {
        var vm = ko.dataFor(this);
        var current = ko.utils.unwrapObservable(vm);
        var stringyF = JSON.stringify(ko.mapping.toJS(current));

        $.ajax({
            beforeSend: function (req) {
                req.setRequestHeader("Accept", "text/calendar");
                req.setRequestHeader("Accept-Encoding", "text");
            },
            type: "GET",
            url: "/api/TimeCard?Id=" + current.Id(),
            contentType: "text/calendar",
            dataType: "text/calendar",
            success: function (response) {
                alert(response.d);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    }
    );
});
