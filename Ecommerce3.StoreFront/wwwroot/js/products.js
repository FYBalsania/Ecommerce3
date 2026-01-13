if (document.readyState !== "loading") {
    document_ready();
} else {
    document.addEventListener("DOMContentLoaded", document_ready);
}


function document_ready() {
    document.getElementById("load_more").addEventListener("click", load_more_clicked);
    document.getElementById("sort_order").addEventListener("change", sort_order_changed);
}

async function sort_order_changed(event) {
    await get_products_by_params(1);
}

async function get_products_by_params(pageNumber) {
    //Get values from html elements.
    const categoryId = document.getElementById("category").dataset.id;
    const selectedBrandIds = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-brand-id]:checked')
    ).map(cb => Number(cb.dataset.brandId));
    const totalProducts = Number(document.getElementById("products_count").value);
    const pageSize = Number(document.getElementById("page_size").value);
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
    params.append("pageNumber", (pageNumber).toString());
    params.append("sortOrder", sortOrder);

    //Construct url.
    const url = new URL("/Products/GetByParams", window.location.origin);
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
        alert("Error occured while loading products. Please try again.");
    } else {
        const html = await response.text();
        if (pageNumber === 1) {
            document.getElementById("product_list").outerHTML = html;
        } else {
            document.getElementById("product_list").innerHTML += html;
            //Update page number.
            document.getElementById("page_number").value = (pageNumber + 1).toString();
        }

        //Toggle load more button's visibility.
        if (totalProducts <= (pageNumber + 1) * pageSize)
            document.getElementById("load_more_container").style.display = "none";
        else
            document.getElementById("load_more_container").style.display = "block";
    }
}

async function load_more_clicked() {
    const pageNumber = Number(document.getElementById("page_number").value);
    await get_products_by_params(pageNumber + 1);
}