$(document).ready(function () {

    $('.js-delete').on('click', function () {
        var btn = $(this);
        //console.log(btn);

        Swal.fire({
            title: "Are you sure That You Want To Delete This Game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your Gmme has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        Swal.fire({
                            title: "Oooooops...!!",
                            text: "Somthing Went Wrong!.",
                            icon: "error"
                        });
                    }


                });

            }
        });




    });
});