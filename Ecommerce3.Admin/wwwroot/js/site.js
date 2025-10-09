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