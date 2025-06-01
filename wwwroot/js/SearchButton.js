function checkQuery() {

    query = document.getElementById("query").value;

    if (query.trim().length > 0) {

        document.getElementById("search_button").disabled = false;
    } else {
        document.getElementById("search_button").disabled = true;
    }

}