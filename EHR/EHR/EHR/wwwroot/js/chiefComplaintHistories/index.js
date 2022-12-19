﻿var $btnShowChiefComplaintHistoriesPanel;
var chiefComplaintHistoriesPanel;

var patientCaseService;
var chiefComplaintHistoriesService;

var chiefComplaintHistories;
var patientCaseId = 0;

$(function () {
    initializeComponent();
    pageLoad();
});

function initializeComponent() {
    $btnShowChiefComplaintHistoriesPanel = $("#btnShowChiefComplaintHistoriesPanel")
        .linkbutton().hide();
    $btnShowChiefComplaintHistoriesPanel.on("click", function () {
        $btnShowChiefComplaintHistoriesPanel.hide();
        chiefComplaintHistoriesPanel.open();
        chiefComplaintHistoriesPanel.state = chiefComplaintHistoriesPanel.preState;
        chiefComplaintHistoriesPanel.preState = "minimize";
    });

    chiefComplaintHistoriesPanel = new ChiefComplaintHistoriesPanel();
    chiefComplaintHistoriesPanel.id = "chiefComplaintHistoriesPanel";
    chiefComplaintHistoriesPanel.title = "Chief Complaint and Histories (Subjective)";
    chiefComplaintHistoriesPanel.style = { "margin": "2px 5px" };
    chiefComplaintHistoriesPanel.onBeforeMaximize = function () {
        disableShowBtn();
        closePanelExcept(chiefComplaintHistoriesPanel);
    };
    chiefComplaintHistoriesPanel.onMaximize = function () {

    }
    chiefComplaintHistoriesPanel.onMinimize = function () {
        enableShowBtn();
        $btnShowChiefComplaintHistoriesPanel.show();
        showNormalPanel();
    };
    chiefComplaintHistoriesPanel.onRestore = function () {
        enableShowBtn();
        showNormalPanel();
    }
}

function pageLoad() {
    patientCaseService = new PatientCaseService();
    chiefComplaintHistoriesService = new ChiefComplaintHistoriesService();

    patientCaseId = getIframeQueryString("patientCaseId");
    if (patientCaseId == null || patientCaseId == "undefined") patientCaseId = 0;

    patientCaseService.getOneById(patientCaseId).then(function (data) {
        var chiefComplaintHistoriesId = data.chiefComplaintHistoriesId || 0;
        return chiefComplaintHistoriesService.getOneById(chiefComplaintHistoriesId);
    }).then(function (data) {
        chiefComplaintHistories = data;
        chiefComplaintHistories.patientCaseId = patientCaseId;
        chiefComplaintHistoriesPanel.build(chiefComplaintHistories);
        chiefComplaintHistoriesPanel.maximize();
    });
}

function disableShowBtn() {
    $btnShowChiefComplaintHistoriesPanel.linkbutton("disable");
}

function enableShowBtn() {
    $btnShowChiefComplaintHistoriesPanel.linkbutton("enable");
}

function closePanelExcept(panel) {
    var arrPanel = [chiefComplaintHistoriesPanel];
    arrPanel.forEach(function (item) {
        if (item.id != panel.id) {
            item.close();
        }
    });
}

function showNormalPanel() {
    var arrPanel = [chiefComplaintHistoriesPanel];
    arrPanel.forEach(function (item) {
        if (item.state == "normal") item.open();
    });
}