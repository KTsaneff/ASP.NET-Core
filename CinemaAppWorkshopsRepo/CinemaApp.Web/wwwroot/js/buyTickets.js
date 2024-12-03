$(document).ready(function () {
    $(".buy-ticket-btn").on("click", function () {
        const cinemaId = $(this).data("cinema-id");
        const movieId = $(this).data("movie-id");
        
        $("#cinemaId").val(cinemaId);
        $("#movieId").val(movieId);
        
        $("#buyTicketModal").modal("show");
    });
    
    $("#buyTicketButton").on("click", function () {
        const requestData = {
            cinemaId: $("#cinemaId").val(),
            movieId: $("#movieId").val(),
            quantity: $("#quantity").val()
        };

        $.ajax({
            url: "/api/TicketApi/BuyTicket",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(requestData),
            success: function (response) {
                alert(response);
                $("#buyTicketModal").modal("hide");
            },
            error: function (xhr) {
                const errorMessage = xhr.responseText || "An error occurred.";
                $("#errorMessage").text(errorMessage).removeClass("d-none");
            }
        });
    });
});
