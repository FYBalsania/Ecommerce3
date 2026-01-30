if (document.readyState !== "loading") {
    document_ready();
} else {
    document.addEventListener("DOMContentLoaded", document_ready);
}


function document_ready() {
    document.getElementById("load_more").addEventListener("click", load_more_clicked);
    document.getElementById("sort_order").addEventListener("change", sort_order_changed);
    document.addEventListener('change',  async (event) => {
        if (event.target.matches('[data-entity="Weight"]')) {
            await weight_changed();
        }
    })
}

async function weight_changed() {
    await get_products_by_params(1);
}

async function load_more_clicked() {
    const pageNumber = Number(document.getElementById("page_number").value);
    await get_products_by_params(pageNumber + 1);
}

async function sort_order_changed(event) {
    await get_products_by_params(1);
}

async function get_products_by_params_(pageNumber) {
    //Get values from html elements.
    const categoryId = document.getElementById("category").dataset.id;
    const selectedBrandIds = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-brand-id]:checked')
    ).map(cb => Number(cb.dataset.brandId));
    let totalProducts = Number(document.getElementById("products_count").value);
    const pageSize = Number(document.getElementById("page_size").value);
    const sortOrder = document.getElementById("sort_order").value;
    const requestVerificationToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const weights = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-entity="Weight"]:checked')
    )
        .map(cb => ({
            key: Number(cb.dataset.uomId),
            value: Number(cb.dataset.qtyPerUom)
        }))
        .sort((a, b) => a.key - b.key);

    weights.forEach((w, i) => {
        params.append(`weights[${i}].Key`, w.key);
        params.append(`weights[${i}].Value`, w.value);
    });

    //Construct url query string.
    const params = new URLSearchParams();
    params.append("category", categoryId);
    params.append("brands", selectedBrandIds.join(","));
    params.append("minPrice", null);
    params.append("maxPrice", null);
    params.append("weights", weights);
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
            //Replace product list.
            document.getElementById("product_list").outerHTML = html;
            //fetch total products count from the element because the element is replaced above.
            totalProducts = Number(document.getElementById("products_count").value);
            //Toggle load more button.
            if (totalProducts > pageSize) 
                document.getElementById("load_more").style.display = "block";
            else 
                document.getElementById("load_more").style.display = "none";

        } else {   //load more handling...
            //Append products to a product list.
            document.getElementById("product_list").innerHTML += html;
            //Update page number.
            document.getElementById("page_number").value = (pageNumber + 1).toString();
            //Hide the load more button if all products are loaded.
            if (totalProducts <= (pageNumber + 1) * pageSize)
                document.getElementById("load_more").style.display = "none";
        }
    }
}

async function get_products_by_params(pageNumber) {
    const params = new URLSearchParams();

    // category
    const categoryId = document.getElementById("category").dataset.id;
    params.append("category", categoryId);

    // brands (int[] brands)
    const selectedBrandIds = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-brand-id]:checked')
    ).map(cb => Number(cb.dataset.brandId))
        .sort((a, b) => a - b);
    selectedBrandIds.forEach(b => params.append("brands", b));

    // sort & paging
    const sortOrder = document.getElementById("sort_order").value;
    params.append("pageNumber", pageNumber.toString());
    params.append("sortOrder", sortOrder);

    // weights (List<KeyValuePair<int, decimal>>)
    const weights = Array.from(
        document.querySelectorAll('input[type="checkbox"][data-entity="Weight"]:checked')
    )
        .map(cb => ({
            key: Number(cb.dataset.uomId),
            value: Number(cb.dataset.qtyPerUom)
        }))
        .sort((a, b) => a.key - b.key);

    weights.forEach((w, i) => {
        params.append(`weights[${i}].Key`, w.key);
        params.append(`weights[${i}].Value`, w.value);
    });

    // build URL
    const url = new URL("/Products/GetByParams", window.location.origin);
    url.search = params.toString();

    // keep the URL in sync with filters (shareable / bookmarkable)
    const urlParamsForHistory = new URLSearchParams(params);
    urlParamsForHistory.delete("category");
    const query = urlParamsForHistory.toString();
    const { pathname, hash } = window.location;
    history.replaceState(
        null,
        "",
        query ? `${pathname}?${query}${hash}` : `${pathname}${hash}`
    );

    // antiforgery
    const requestVerificationToken =
        document.querySelector('input[name="__RequestVerificationToken"]').value;

    const response = await fetch(url, {
        method: "GET",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        }
    });

    if (!response.ok) {
        alert("Error occurred while loading products. Please try again.");
        return;
    }

    const html = await response.text();

    if (pageNumber === 1) {
        document.getElementById("product_list").outerHTML = html;
        const totalProducts = Number(document.getElementById("products_count").value);
        const pageSize = Number(document.getElementById("page_size").value);
        
        document.querySelector('.block_count').textContent = `${totalProducts} Products`;

        if (totalProducts > pageSize)
            document.getElementById("load_more").style.display = "block";
        else
            document.getElementById("load_more").style.display = "none";
    } else {
        document.getElementById("product_list").innerHTML += html;
        document.getElementById("page_number").value = (pageNumber + 1).toString();
        const totalProducts = Number(document.getElementById("products_count").value);
        const pageSize = Number(document.getElementById("page_size").value);
        
        if (totalProducts <= (pageNumber + 1) * pageSize)
            document.getElementById("load_more").style.display = "none";
    }
}


