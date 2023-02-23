function GoToGenre(ID) {

        $.ajax({
        type: "Post",
        dataType: "Json",
        url: '/Home/ToGenre',
        data: JSON.stringify(ID),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function GoToSongsGenre(ID) {

    $.ajax({
        type: "Post",
        dataType: "Json",
        url: '/Home/GoToGenre',
        data: JSON.stringify(ID),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function AddToQueue(ID) {

    $.ajax({
        type: "Post",
        dataType: "Json",
        url: '/Queue/AddtoQueue',
        data: JSON.stringify(ID),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function GoToCreate() {

    $.ajax({
        type: "Post",
        dataType: "Json",
        url: '/User/toCreate',
        data: JSON.stringify(null),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}