/// <reference path="_references.js" />
var viewModel = {
    timecards: ko.observableArray([]),
    selectedTimeCard: ko.observable(),
    addTimeCard: function ()
    {
        viewModel.timecards.push(addNewTimeCard());
    },
    selectTimeCard: function ()
    {
        viewModel.selectedTimeCard(this);
    },
    commitAll: function ()
    {
        viewModel.selectedTimeCard().Summary.commit();
        viewModel.selectedTimeCard().Description.commit();
        viewModel.selectedTimeCard().StartDate.commit();
        viewModel.selectedTimeCard().EndDate.commit();
        viewModel.selectedTimeCard().Status.commit();

    }
};

function addNewTimeCard(timeCard)
{
    if (timeCard == null)
    {
        return {
            Id: ko.protectedObservable(0),
            Summary: ko.protectedObservable("[Summary]"),
            Description: ko.protectedObservable("[Description]"),
            StartDate: ko.protectedObservable((new Date()).toJSON()),
            EndDate: ko.protectedObservable((new Date()).toJSON()),
            Status: ko.protectedObservable("Tentative")
        };
    }
    else
    {
        return {
            Id: ko.protectedObservable(timeCard.Id),
            Summary: ko.protectedObservable(timeCard.Summary),
            Description: ko.protectedObservable(timeCard.Description),
            StartDate: ko.protectedObservable((new Date(timeCard.StartDate)).toJSON()),
            EndDate: ko.protectedObservable((new Date(timeCard.EndDate)).toJSON()),
            Status: ko.protectedObservable(timeCard.Status)
        };
    }
}