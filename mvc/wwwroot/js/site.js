function Create() {

    var name = $("#Name").val();
    var credit = $("#Credit").val();

    $.ajax({
        url: '/Home/Create',
        type: 'POST',
        dataType: 'json',
        data: {Name: name, Credit : credit },
        success: function (response) {
            $("#tableBody").prepend(
                "<tr id=" + response.id+">"+
                "<th scope='row'>"+ response.id+"</th>"+
                "<td>" + response.name +"</td>"+
                "<td>" + response.credit + "</td>" +
                "<th scope='col'><button onclick=\"Delete(\'"+response.id+"\')\"> <span class='glyphicon glyphicon-remove'></span>Remove</button ></th > " +
                "</tr>"
            );
        },
        error: function (hata, ajaxOptions, thrownError) {
            alert(hata.status);
            alert(thrownError);
            alert(hata.responseText);
        }
    });
}

function Delete(id) {


    $.ajax({
        url: '/Home/Delete',
        type: 'DELETE',
        dataType: 'json',
        data: { id: id },
        success: function (response) {
            $("#"+ id).remove();
            
        },
        error: function (hata, ajaxOptions, thrownError) {
     
            alert(hata.responseText);
        }
    });
}
