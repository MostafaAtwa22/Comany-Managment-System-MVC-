$(document).ready(function () {
    $(".search").on("keyup", function () {
        var searchTerm = $(this).val().toLowerCase();
        var employeeCount = 0;

        $(".result-item").each(function () {
            var employeeName = $(this).find("h5.text-dark").text().toLowerCase();
            if (employeeName.includes(searchTerm)) {
                $(this).attr("visible", "true").show();
                employeeCount++;
            } else {
                $(this).attr("visible", "false").hide();
            }
        });

        $(".counter").text(employeeCount + " item" + (employeeCount !== 1 ? "s" : ""));

        if (employeeCount === 0) {
            $(".results").hide(); 
            $(".no-result").show(); 
        } else {
            $(".results").show(); 
            $(".no-result").hide(); 
        }
    });
});
