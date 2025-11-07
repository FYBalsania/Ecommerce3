// Sidebar toggle functionality
const sidebarToggle = document.getElementById('sidebarToggle');
const sidebar = document.getElementById('sidebar');
const mainContent = document.getElementById('mainContent');

sidebarToggle.addEventListener('click', function() {
    sidebar.classList.toggle('collapsed');
    mainContent.classList.toggle('expanded');
});

// Handle sidebar navigation
const navLinks = document.querySelectorAll('.nav-link[data-section]');
navLinks.forEach(link => {
    link.addEventListener('click', function(e) {
        e.preventDefault();

        // Remove active class from all links
        navLinks.forEach(l => l.classList.remove('active'));

        // Add active class to clicked link
        this.classList.add('active');

        // Here you would typically load the content for the selected section
        console.log('Loading section:', this.dataset.section);
    });
});

function toSlug(name) {
    // Convert to lowercase
    let slug = name.toLowerCase();

    // Replace spaces with hyphens
    slug = slug.replace(/\s+/g, "-");

    // Remove disallowed characters (except - _ . ~)
    slug = slug.replace(/[^a-z0-9\-_~.]/g, "");

    // Trim leading and trailing hyphens
    slug = slug.replace(/^-+|-+$/g, "");

    // Collapse multiple consecutive hyphens into a single one
    slug = slug.replace(/-+/g, "-");

    return slug;
}

function isHttpsOrRelativeUrl(url) {
    // Allow relative URLs (e.g. "/path", "../img", "?q=1", "#anchor")
    if (/^(\/(?!\/)|\.{1,2}\/|\?|#)/.test(url)) {
        return true;
    }

    // Allow protocol-relative URLs (e.g. "//example.com")
    if (/^\/\//.test(url)) {
        return true;
    }

    try {
        const parsed = new URL(url);
        return parsed.protocol === 'https:';
    } catch {
        return false;
    }
}

function doAjax(url, method, data, processData = false, contentType = false, traditional = false) {
    return $.ajax({
        url: url,
        type: method,
        data: data,
        cache: false,
        processData: processData,
        contentType: contentType,
        traditional: traditional,
    });
}

function isValidNumberStrict(value) {
    if (typeof value !== 'string' && typeof value !== 'number') return false;

    const str = String(value).trim();

    // Must match optional '-', digits, optional '.', digits — but not '.' alone or incomplete decimals
    const pattern = /^-?\d+(\.\d+)?$/;

    if (!pattern.test(str)) return false;

    // Additional check — ensures it's a finite number
    const num = Number(str);
    return Number.isFinite(num);
}