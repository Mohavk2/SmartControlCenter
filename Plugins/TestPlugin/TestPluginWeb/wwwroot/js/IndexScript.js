$(document).ready(function () {

    /////*Ajax*/////

    $('#TestAjaxButton').click(async function () {

        const response = await fetch("/Test/api/TestAjax", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok === true) {
            $('#TestAjaxOutput').html(await response.json());
        }
    });

    /////*Connections*/////

    //Command connection
    const commandConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestCommand")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    commandConnection.on("MoveCircle", (x, y) => {
        $('#Circle').css('left', x + "px");
        $('#Circle').css('top', y + "px");
        $('#CircleCoordinates').html(x + "px : " + y + "px");
    });

    commandConnection.start();

    //Action connection
    const actionConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestAction")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    actionConnection.on("Add", action => {
        if ($('#CircleMoveRecording').prop('checked')) {

            let selectors = $('.ActionSelector');
            for (var i = 0; i < selectors.length; i++) {
                let opt = document.createElement('option');
                opt.value = action.id;
                opt.innerHTML = action.name;
                selectors[i].append(opt);
            }
            $('#CircleMoveRecording').prop('checked', false);
        }
    });

    actionConnection.on("Run", action => {
        $('#ActionOutput').html(action.name + " is runing");
    });

    actionConnection.start();

    //Script connection
    const scriptConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestScript")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    scriptConnection.on("Script1Running", message => {
        $('#ScriptOutput').html(message);
    });

    scriptConnection.start();

    /////*Circle*/////

    var mouseDifferenceX = 0;
    var mouseDifferenceY = 0;
    var mousedown = false;
    var commandBuffer = new Array();

    $('#Circle').mousedown(function (e) {
        mousedown = true;
        let offset = $('#Circle').offset();
        mouseDifferenceX = e.clientX - offset.left;
        mouseDifferenceY = e.clientY - offset.top;
    });

    $('#Circle').mouseup(function (e) {
        mousedown = false;
        if ($('#CircleMoveRecording').prop('checked')) {
            actionConnection.invoke("Add", {
                "Id": parseInt(0),
                "Name": prompt("type action name"),
                "Repeat": parseInt(1),
                "Delay": parseInt(0),
                "Commands": commandBuffer
            });
            commandBuffer = new Array();
        }
    });

    $('#CircleContainer').mousemove(function (e) {
        if (mousedown) {
            let x = (e.clientX - mouseDifferenceX - $('#CircleContainer').offset().left).toString();
            let y = (e.clientY - mouseDifferenceY - $('#CircleContainer').offset().top).toString();
            $('#Circle').css('left', x + 'px');
            $('#Circle').css('top', y + 'px');
            $('#CircleCoordinates').html(x + " : " + y);
            if ($('#CircleMoveRecording').prop('checked')) {
                commandBuffer.push({
                    Delay: parseInt(17),
                    Method: "MoveCircle",
                    Parameters: [x, y]
                });
            }
        }
    });

    /////*Command panel*/////

    $('#MoveCircle').click(function () {
        commandConnection.invoke("MoveCircle", [$('#InputX').val(), $('#InputY').val()]);
    });

    /////*Action panel*/////

    $('#RunAction').click(function (e) {
        actionConnection.invoke("Run", parseInt($('#ActionSelector1').val()));
    });

    /////*Script panel*/////

    $('#TestScript').click(function () {
        scriptConnection.invoke("RunScript1", $('#ScriptInput').val());
    });
});


