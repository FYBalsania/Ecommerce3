const burgerMenu = document.getElementById("burgerMenu");
const mainNav = document.getElementById("mainNav");
const overlay = document.getElementById("menuOverlay");
const navItems = Array.from(document.querySelectorAll(".nav-item"));

// Safety guards
if (!burgerMenu || !mainNav || !overlay) {
  console.warn("Menu elements missing - burger/mainNav/overlay");
}

// Helper: is touch device? (using what-input)
function isTouchDevice() {
  return document.documentElement.getAttribute("data-whatinput") === "touch";
}

// Add / remove "has-submenu" once on load
navItems.forEach((item) => {
  const navLink = item.querySelector(".nav-link");
  const megaMenu = item.querySelector(".mega-menu");
  if (!navLink) return;

  if (megaMenu) {
    navLink.classList.add("has-submenu");
  } else {
    navLink.classList.remove("has-submenu");
  }
});

// Helper: close everything
function closeAllMenus() {
  burgerMenu && burgerMenu.classList.remove("active");
  mainNav && mainNav.classList.remove("active");
  overlay && overlay.classList.remove("active");

  // Only touch elements that are currently active
  document
    .querySelectorAll(".nav-item.active, .mega-menu-column.active")
    .forEach((el) => el.classList.remove("active"));
}

// Toggle mobile menu (burger)
burgerMenu?.addEventListener("click", () => {
  burgerMenu.classList.toggle("active");
  mainNav.classList.toggle("active");
  overlay.classList.toggle("active");

  // When opening, ensure all submenus are reset
  if (burgerMenu.classList.contains("active")) {
    document
      .querySelectorAll(".nav-item.active, .mega-menu-column.active")
      .forEach((el) => el.classList.remove("active"));
  }
});

// Close menu when overlay is clicked
overlay?.addEventListener("click", () => {
  closeAllMenus();
});

// Single delegated handler for nav (submenus + columns)
mainNav?.addEventListener("click", (e) => {
  if (!isTouchDevice()) {
    // On non-touch devices, let :hover/CSS handle mega menu
    return;
  }

  // 1) Handle column heading taps first
  const heading = e.target.closest(".mega-menu-column > h3");
  if (heading) {
    e.preventDefault();
    e.stopPropagation();

    const column = heading.parentElement;
    if (!column) return;

    column.classList.toggle("active");
    return; // Don’t fall through to nav-link handling
  }

  // 2) Handle nav-link taps for items with mega menu
  const navLink = e.target.closest(".nav-link.has-submenu");
  if (navLink) {
    e.preventDefault();

    const item = navLink.closest(".nav-item");
    if (!item) return;

    const isAlreadyActive = item.classList.contains("active");

    // Close everything else
    navItems.forEach((other) => {
      if (other !== item) {
        other.classList.remove("active");
        other
          .querySelectorAll(".mega-menu-column.active")
          .forEach((col) => col.classList.remove("active"));
      }
    });

    // Toggle current item
    if (isAlreadyActive) {
      item.classList.remove("active");
      item
        .querySelectorAll(".mega-menu-column.active")
        .forEach((col) => col.classList.remove("active"));
    } else {
      item.classList.add("active");
    }
  }
});

// Handle window resize - close menu if going to desktop
window.addEventListener("resize", () => {
  if (window.innerWidth > 1023) {
    closeAllMenus();
  }
});
