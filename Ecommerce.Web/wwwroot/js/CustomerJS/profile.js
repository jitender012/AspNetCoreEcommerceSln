 
$(function () {
    $("#v-pills-profile").load("/Customer/Account/Profile");


    //user address methods----------
    $("#v-pills-address-tab").one('click', function () {
        $("#v-pills-address").load("/Customer/Address/Index");
    })

    $("#addressForm").on("submit", function (e) {
        alert("hi")
        e.preventDefault();

        $.ajax({
            url: "/Customer/Address/AddAddress",
            type: "POST",
            data: $(this).serialize(),
            success: function (res) {
                console.log(res);
                $("#v-pills-address").load("/Customer/Address/Index");
            }
        });
    });



});

