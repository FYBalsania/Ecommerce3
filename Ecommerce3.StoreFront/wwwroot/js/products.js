if (document.readyState !== "loading") {
    document_ready();
} else {
    document.addEventListener("DOMContentLoaded", document_ready);
}


function document_ready() {
    document.getElementById("load_more").addEventListener("click", load_more_clicked);
}

async function load_more_clicked(){
    //Get values from html elements.
    const categoryId = document.getElementById("category").dataset.id;
    const selectedBrandIds = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-brand-id]:checked')
    ).map(cb => Number(cb.dataset.brandId));
    const pageNumber = document.getElementById("page_number").value;
    const sortOrder = document.getElementById("sort_order").value;
    const requestVerificationToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
    
    //Construct url query string.
    const params = new URLSearchParams();
    params.append("category", categoryId);
    params.append("brands", selectedBrandIds.join(","));
    params.append("minPrice", null);
    params.append("maxPrice", null);
    params.append("weights", null);
    params.append("attributes", null);
    params.append("pageNumber", (Number(pageNumber) + 1).toString());
    params.append("sortOrder", sortOrder);

    //Construct url.
    const url = new URL("/Products/NextPage", window.location.origin);
    url.search = params.toString();
    
    //Call endpoint.
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        }
    });
    
    //Process response.
    if (!response.ok) {
        alert("Error occured while loading products.");
    }
    else {
        const html = await response.text();
        document.getElementById("product_list").innerHTML += html;
    }
}