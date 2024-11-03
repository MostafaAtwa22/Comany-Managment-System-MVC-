$(document).ready(function () {
    $(".search").on("keyup", function () {
        var searchTerm = $(this).val().toLowerCase();
        var jobCount = 0;

        $(".result-item").each(function () {
            var Name = $(this).find(".card-title").text().toLowerCase();
            if (Name.includes(searchTerm)) {
                $(this).attr("visible", "true").show();
                jobCount++;
            } else {
                $(this).attr("visible", "false").hide();
            }
        });

        $(".counter").text(jobCount + " item" + (jobCount !== 1 ? "s" : ""));
        if (jobCount === 0) {
            $(".no-result").show();
        } else {
            $(".no-result").hide();
        }
    });
});