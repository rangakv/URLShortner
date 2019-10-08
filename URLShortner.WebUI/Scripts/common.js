    $(document).ready(function () {
    $(function () {
        $('#btnHashUrl').click(function (event) {
            alert($("#txtLongUrl").val());
            event.preventDefault();
            var sUrl = $("#txtLongUrl").val();
            $.ajax({
                type: "GET",
                url: "/Home/HashUrl?sUrl=" + sUrl,
                success: function (data) {
                    alert(data.msg);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        });
    });
});
        