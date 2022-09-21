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
        url: '/Home/GoToSongsGenres',
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
        url: '/Home/toCreate',
        data: JSON.stringify(null),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function LoginToUser(ID) {

    $.ajax({
        type: "Post",
        dataType: "Json",
        url: '/Home/ToLogin',
        data: JSON.stringify(null),
        contentType: "application/json",
        success: function () { console.log(1); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); },
        complete: function () { location.reload(); }
    });
}