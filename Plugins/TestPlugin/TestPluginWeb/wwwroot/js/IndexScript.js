$(document).ready(function () {

    /////*Ajax*/////

    $('#testAjaxButton').click(async function () {

        const response = await fetch("/Test/api/TestAjax", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok === true) {
            $('#testAjaxOutput').html(await response.json());
        }
    });

    /////*Connections*/////

    //Command connection
    const commandConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestCommand")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    commandConnection.on("moveCircle", (x, y) => {
        $('#circle').css('left', x + "px");
        $('#circle').css('top', y + "px");
        $('#circleCoordinates').html(x + "px : " + y + "px");
    });

    commandConnection.start();

    //Action connection
    const actionConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestAction")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    actionConnection.on("Add", action => {
        if ($('#recordCircleMoving').prop('checked')) {

            let selectors = $('.action-selector');
            for (var i = 0; i < selectors.length; i++) {
                let opt = document.createElement('option');
                opt.value = action.id;
                opt.innerHTML = action.name;
                selectors[i].append(opt);
            }
            $('#recordCircleMoving').prop('checked', false);
        }
    });

    actionConnection.on("Run", action => {
        $('#actionName').html(action.name + " is runing");
    });

    actionConnection.start();

    //Script connection
    const scriptConnection = new signalR.HubConnectionBuilder()
        .withUrl("/TestScript")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    scriptConnection.on("ScriptRunning", message => {
        $('#ScriptOutput').html(message);
    });

    scriptConnection.start();

    /////*Circle*/////

    var mouseDifferenceX = 0;
    var mouseDifferenceY = 0;
    var mousedown = false;
    var commandBuffer = new Array();

    $('#circle').mousedown(function (e) {
        mousedown = true;
        let offset = $('#circle').offset();
        mouseDifferenceX = e.clientX - offset.left;
        mouseDifferenceY = e.clientY - offset.top;
    });

    $('#circle').mouseup(function (e) {
        mousedown = false;
        if ($('#recordCircleMoving').prop('checked')) {
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

    $('#circleContainer').mousemove(function (e) {
        if (mousedown) {
            let x = (e.clientX - mouseDifferenceX - $('#circleContainer').offset().left).toString();
            let y = (e.clientY - mouseDifferenceY - $('#circleContainer').offset().top).toString();
            $('#circle').css('left', x + 'px');
            $('#circle').css('top', y + 'px');
            $('#circleCoordinates').html(x + " : " + y);
            if ($('#recordCircleMoving').prop('checked')) {
                commandBuffer.push({
                    Delay: parseInt(17),
                    Method: "MoveCircle",
                    Parameters: [x, y]
                });
            }
        }
    });

    /////*Command panel*/////

    $('#moveCircle').click(function () {
        commandConnection.invoke("moveCircle", [$('#inputX').val(), $('#inputY').val()]);
    });

    /////*Action panel*/////

    $('#runAction').click(function (e) {
        actionConnection.invoke("Run", parseInt($('#actionPanelActionSelector').val()));
    });

    /////*Script panel*/////

    var itemId = 0;

    $('#addAction').click(function (e) {

        let scriptId = $('#scriptBuilderActionSelector').val();
        let scriptName = $('#scriptBuilderActionSelector option:selected').text();

        $('#actionList').append(
            '<div class="action-list-item">'
            + `<span class="action-list-item__item-id">${itemId++}</span>`
            + `<span class="action-list-item__script-id">${scriptId}</span>`
            + `<span class="action-list-item__name"> ${scriptName} </span>`
            + '<div class="action-list-item__options">'
            + '<div class="action-list-item__col-1">'
            + '<div>delay</div>'
            + '<div>repeat</div>'
            + '</div>'
            + '<div class="action-list-item__col-2">'
            + '<input type="number" id="delay" value="0"/>'
            + '<input type="number" id="repeat" value="1"/>'
            + '</div>'
            + '</div>'
            + '</div>');
    });

    $('#runScript').click(function () {
        scriptConnection.invoke("Run", $('#ScriptInput').val());
    });
});


