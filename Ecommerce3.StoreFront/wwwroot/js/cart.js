// Initialize pricing structure from HTML data-value attributes
function initializePricing() {
  const pricing = {};

  // Get all weight radio inputs
  const weightInputs = document.querySelectorAll('input[name="weight"]');

  weightInputs.forEach((input) => {
    const weightValue = input.value; // e.g., "250g", "500g", etc.
    const price = parseFloat(input.getAttribute("data-value")) || 0;

    // Extract numeric weight from value (e.g., "250g" -> 250)
    const weightMatch = weightValue.match(/(\d+)/);
    const weight = weightMatch ? parseInt(weightMatch[1]) : 0;

    // Convert to grams if it's in kg
    const weightInGrams = weightValue.includes("kg") && !weightValue.includes("25kg") ? weight * 1000 : weightValue === "25kg" ? 25000 : weight;

    pricing[weightValue] = {
      weight: weightInGrams,
      basePrice: price,
      pricePerGram: price / weightInGrams,
    };
  });

  sessionStorage.setItem("pricingData", JSON.stringify(pricing));
  return pricing;
}

// Get current pricing data
function getPricingData() {
  const stored = sessionStorage.getItem("pricingData");
  return stored ? JSON.parse(stored) : initializePricing();
}

// Get selected weight option
function getSelectedWeight() {
  const selected = document.querySelector('input[name="weight"]:checked');
  return selected ? selected.value : "250g";
}

// Get current quantity
function getQuantity() {
  const qtyInput = document.getElementById("quantity");
  return parseInt(qtyInput.value) || 1;
}

// Calculate and update all prices
function updatePrices() {
  const pricing = getPricingData();
  const selectedWeight = getSelectedWeight();
  const quantity = getQuantity();

  const weightData = pricing[selectedWeight];
  if (!weightData) return;

  // Calculate total price
  const totalPrice = weightData.basePrice * quantity;
  const pricePerGram = weightData.pricePerGram;
  const pricePer100g = pricePerGram * 100;

  // Update current price display
  const currentPriceEl = document.querySelector(".current-price");
  if (currentPriceEl) {
    currentPriceEl.textContent = `£${totalPrice.toFixed(2)}`;
  }

  // Update price per 100g display
  const ppkgEl = document.querySelector(".ppkg");
  if (ppkgEl) {
    ppkgEl.textContent = `£${pricePer100g.toFixed(2)} per 100g`;
  }

  // Store current state in session storage
  sessionStorage.setItem("currentWeight", selectedWeight);
  sessionStorage.setItem("currentQuantity", quantity);
  sessionStorage.setItem("currentTotalPrice", totalPrice.toFixed(2));
}

// Update cart badge and value
function updateCart() {
  // Get cart data from session storage
  const cartQuantity = parseInt(sessionStorage.getItem("cartQuantity")) || 0;
  const cartTotal = parseFloat(sessionStorage.getItem("cartTotal")) || 0;

  // Update all cart badges (both desktop and mobile)
  const cartBadges = document.querySelectorAll(".cart-badge");
  cartBadges.forEach((badge) => {
    badge.textContent = cartQuantity;
  });

  // Update all cart values (both desktop and mobile)
  const cartBtns = document.querySelectorAll(
    ".cart-btn span, .cart-btn-mob span"
  );
  cartBtns.forEach((span) => {
    // Only update spans that contain price (not the badge)
    if (!span.classList.contains("cart-badge")) {
      span.textContent = `£${cartTotal.toFixed(2)}`;
    }
  });
}

// Add to basket function
function addToBasket(event) {
  // Prevent any default behavior
  if (event) {
    event.preventDefault();
    event.stopPropagation();
  }

  const quantity = getQuantity();
  const totalPrice =
    parseFloat(sessionStorage.getItem("currentTotalPrice")) || 0;

  // Get existing cart data
  const currentCartQty = parseInt(sessionStorage.getItem("cartQuantity")) || 0;
  const currentCartTotal = parseFloat(sessionStorage.getItem("cartTotal")) || 0;

  // Add current selection to cart
  const newCartQty = currentCartQty + quantity;
  const newCartTotal = currentCartTotal + totalPrice;

  // Update cart data in session storage
  sessionStorage.setItem("cartQuantity", newCartQty);
  sessionStorage.setItem("cartTotal", newCartTotal.toFixed(2));

  // Update cart display
  updateCart();

  // Optional: Remove this alert or replace with a nicer notification
  // alert(`Added ${quantity} item(s) to basket!`);
}

// Increase quantity
function increaseQuantity() {
  const qtyInput = document.getElementById("quantity");
  const currentQty = parseInt(qtyInput.value) || 1;
  qtyInput.value = currentQty + 1;
  updatePrices();
}

// Decrease quantity
function decreaseQuantity() {
  const qtyInput = document.getElementById("quantity");
  const currentQty = parseInt(qtyInput.value) || 1;
  if (currentQty > 1) {
    qtyInput.value = currentQty - 1;
    updatePrices();
  }
}

// Handle manual quantity input change
function handleQuantityInput() {
  const qtyInput = document.getElementById("quantity");
  let value = parseInt(qtyInput.value);

  if (isNaN(value) || value < 1) {
    value = 1;
  }

  qtyInput.value = value;
  updatePrices();
}

// Initialize on page load
document.addEventListener("DOMContentLoaded", function () {
  // Initialize pricing data
  initializePricing();

  // Add event listeners to weight options
  const weightInputs = document.querySelectorAll('input[name="weight"]');
  weightInputs.forEach((input) => {
    input.addEventListener("change", updatePrices);
  });

  // Add event listener to quantity input for manual changes
  const qtyInput = document.getElementById("quantity");
  if (qtyInput) {
    qtyInput.addEventListener("change", handleQuantityInput);
    qtyInput.addEventListener("input", handleQuantityInput);
  }

  // Initial price update
  updatePrices();

  // Initial cart display update (shows existing cart data if any)
  updateCart();
});

// Make functions globally accessible
window.increaseQuantity = increaseQuantity;
window.decreaseQuantity = decreaseQuantity;
window.addToBasket = addToBasket;
