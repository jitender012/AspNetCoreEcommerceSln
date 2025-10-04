
<script>

    $(document).ready(
    function () {
        $('#myTable').DataTable();
        }
    )
    $(document).ready(function () {
        $(".delete-brand").click(function (e) {
            e.preventDefault();
            var brandId = $(this).data("id");
            var dataRow = $('#' + brandId);
            if (confirm("Are you sure you want to delete this brand?")) {
                $.ajax({
                    type: "POST",
                    url: '@Url.Action("Delete", "Brand")',
                    data: { brandId: brandId },
                    success: function (response) {
                        if (response.success) {
                            alert(response.message);
                            dataRow.remove();
                        } else {
                            alert(response.message);
                        }
                    },
                    error: function () {
                        alert("An error occurred while deleting the brand. Please try again.");
                    }
                });
            }
        });
    });
    // $(document).ready(
    // function () {
    //     alert("Hi");
    //     }
    // )

</script>


