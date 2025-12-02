// Initialize Splide slider when DOM is loaded

function handleFilterClick(e) {
  const accordion = document.querySelector(".accordion");
  e.preventDefault();
  let overlay = document.querySelector(".overlay");
  if (!overlay) {
    overlay = document.createElement("div");
    overlay.classList.add("overlay");
  }
  const filterButton = e.target;
  const filters = document.querySelector(".filters");
  if (!filterButton.classList.contains("active")) {
    document.body.appendChild(overlay);
    overlay.style.opacity = 0.7;
    overlay.style.visibility = "visible";
    filterButton.classList.add("active");
    filters.classList.add("active");
    filterButton.innerHTML = `<img src="images/bw-filter-icon.svg" alt="Close"> Close`;
    filterButton.disabled = true;
    setTimeout(() => {
      filterButton.disabled = false;
    }, 300);
    document.body.classList.toggle("overflow-hidden");
  } else {
    setTimeout(() => {
      document.body.removeChild(overlay);
      filterButton.disabled = false;
    }, 300);
    filterButton.disabled = true;
    overlay.style.opacity = 0;
    overlay.style.visibility = "hidden";
    filterButton.classList.remove("active");
    filters.classList.remove("active");
    filterButton.innerHTML = `<img src="images/bw-filter-icon.svg" alt="Filter"> Filters`;
    document.body.classList.remove("overflow-hidden");
  }

  if (overlay) {
    overlay.addEventListener("click", () => {
      console.log(overlay)
      filterButton.classList.remove("active");
      filterButton.innerHTML = `<img src="images/bw-filter-icon.svg" alt="Filter"> Filters`;
      filters.classList.remove("active");
      document.body.classList.remove("overflow-hidden");
      filterButton.disabled = true;
      setTimeout(() => {
        overlay.style.opacity = 0;
        overlay.style.visibility = "hidden";
        filterButton.disabled = false;
        setTimeout(() => {
          document.body.removeChild(overlay);
        }, 300);
      }, 300);
    });
  }
  if (accordion) {
    const accordionItems = accordion.querySelectorAll(".accordion-item");
    accordionItems.forEach((item) => {
      const accordionTitle = item.querySelector(".accordion-title");
      const accordionContent = item.querySelector(".accordion-content");
      const loadMorebtn = item.querySelector(".filter-loadmore");
      accordionTitle.setAttribute("aria-expanded", "false");      
      accordionContent.setAttribute("aria-hidden", "true");
      accordionContent.style.height = "0px";
      if (loadMorebtn) {
        loadMorebtn.style.display = "none";
      }
    });
  }
}

function setupAccordion(accordionContainer) {
  let targets;
  function closeAllPanels() {
    let openPanels = targets.nextElementSibling;
    if (openPanels) {
      openPanels.setAttribute("aria-hidden", "true");
      targets.setAttribute("aria-expanded", "false");
    }
  }
  accordionContainer.addEventListener("click", function (event) {
    if (event.target.getAttribute("role") === "button") {
      let target = event.target;
      targets = target;
      let mainElement = target.nextElementSibling.firstElementChild;
      mainElement.scrollTop = 0;
      let showMore = target.nextElementSibling.nextElementSibling;

      if (target.closest) {
        target = target.closest('[class*="accordion-title"]');
      }
      if (target) {
        if (target.getAttribute("aria-expanded") === "false") {
          toggleExpanded(target, true);
          if (showMore) {
            showMore.style.cssText =
              "display:flex; opacity:1; visibility:visible";
          }
        } else {
          setTimeout(() => {
            target.nextElementSibling.classList.remove("hide");
            target.nextElementSibling.style.height = 0 + "px";
            closeAllPanels();
            if (showMore) {
              showMore.style.display = "none";
            }
          }, 200);
          console.log(target.nextElementSibling);
          target.nextElementSibling.style.height =
            target.nextElementSibling.offsetHeight + "px";
          target.nextElementSibling.classList.add("hide");
          //toggleExpanded(target, false);
          if (showMore) {
            showMore.style.cssText = "opacity:0; visibility:hidden";
          }
        }
      }
    }
  });
}

function toggleExpanded(element, show) {
  var target = document.getElementById(element.getAttribute("aria-controls"));
  console.log(target);
  if (target) {
    target.removeAttribute("style");
    element.setAttribute("aria-expanded", show);
    target.setAttribute("aria-hidden", !show);
  }
}

function allAccordions() {
  let accordionItem = document.querySelectorAll(".accordion-item");
  accordionItem.forEach((item) => {
    let innerUl = item.querySelector("ul");
    if (innerUl?.scrollHeight > 250) {
      const span = document.createElement("span");
      span.classList.add("filter-loadmore");
      const text = document.createTextNode("Show More");
      span.appendChild(text);
      item.appendChild(span);

      span.addEventListener("click", () => {
        innerUl.scrollTop = 0;
        item.classList.toggle("showAll");
        if (item.classList.contains("showAll")) {
          span.textContent = "Show Less";
        } else {
          span.textContent = "Show More";
        }
      });
    }
  });
}

function InsideAccordionToggle(element, show) {
  let target = document.getElementById(element.getAttribute("aria-controls"));
  if (target) {
    element.setAttribute("aria-expanded", show);
    target.setAttribute("aria-hidden", !show);
    target.style.opacity = show ? 1 : 0;
    if (show) {
      target.style.height = target.scrollHeight + "px";
    } else {
      target.style.height = target.scrollHeight + "px";
      target.offsetHeight;
      setTimeout(() => {
        target.style.height = 0;
      }, 200);
    }
  }
}

function setupInsideAccordion(accordionContainer) {
  function closeAllPanels() {
    let openPanels = event.target.nextElementSibling;
    if (openPanels) {
      InsideAccordionToggle(openPanels, false);
    }
  }
  accordionContainer.addEventListener("click", function (event) {
    let target = event.target;

    if (target.closest) {
      target = target.closest('[class*="accordion-title"]');
    }

    if (target) {
      let isTargetOpen = target.getAttribute("aria-expanded") === "true";
      closeAllPanels();
      InsideAccordionToggle(target, !isTargetOpen);
    }
  });
}





document.addEventListener('DOMContentLoaded', function() {
  const productListing = document.getElementById("listing");
  if (productListing) {
  function deliverySelection() {
    const filterButton = document.querySelector(".filter_button");
    if (filterButton) {
      filterButton.removeEventListener("click", handleFilterClick);
      filterButton.addEventListener("click", handleFilterClick);
    }
    
  }
  
  let accordions = document.querySelectorAll(".accordion");
  accordions.forEach(accordion => setupAccordion(accordion));
  allAccordions();
  deliverySelection();
  
  
}


  const bars = document.querySelectorAll('.sm-review-bar-fill');
  bars.forEach(bar => {
      const percent = bar.getAttribute('data-percent');
      setTimeout(() => {
          bar.style.width = percent + '%';
      }, 100);
  });

    document.querySelector('.action-item.search').addEventListener('click', function (e) {
      e.preventDefault();
      const searchBar = document.querySelector('.search-bar');
      searchBar.classList.toggle('active');
    });

    var featureSlider = new Splide('#featureSlider', {
        perPage: 4,
        autoplay: false,
        interval: 3000,
        speed: 1000,
        pagination: false,
        pauseOnHover: true,
        pauseOnFocus: true,
        arrows: false,
        lazyLoad: 'nearby',
        breakpoints: {
            1023: {
                type: 'loop',
                autoplay: true,
                perPage: 1,
                
            },
            639: {
                type: 'loop',
                autoplay: true,
                perPage: 1,
            }
        }
    });

    featureSlider.mount();
      

    // ===== THUMBNAIL CAROUSEL FOR PRODUCT DETAILS PAGE =====
    const thumbnailCarouselElement = document.getElementById('thumbnailCarousel');
    if (thumbnailCarouselElement) {
        
        var thumbnailSplide = new Splide('#thumbnailCarousel', {
            type: 'slide',
            perPage: 1,
            perMove: 1,
            gap: '0.5rem',
            pagination: false,
            arrows: true,
            drag: true,
            focus: '0',
            trimSpace: false,
            breakpoints: {
                1023: { perPage: 3, gap: '0.5rem' },
                639: { perPage: 2, gap: '0.5rem' },
                360: { perPage: 2, gap: '0.4rem' }
            }
        });
        
        thumbnailSplide.mount();

        // Get all thumbnail images
        const thumbnails = document.querySelectorAll('.plight-box-thumbnail');
        const images = Array.from(thumbnails).map(thumb => thumb.dataset.lightbox);

        // Function to update main image
        function updateMainImage(index) {
            if (thumbnails[index]) {
                const img = thumbnails[index];
                const mainImage = document.getElementById('mainImage');
                const mainSourceLarge = document.getElementById('mainSourceLarge');
                const mainSourceSmall = document.getElementById('mainSourceSmall');
                
                if (mainImage && mainSourceLarge && mainSourceSmall) {
                    mainSourceLarge.srcset = img.dataset.large;
                    mainSourceSmall.srcset = img.dataset.medium;
                    mainImage.src = img.dataset.large;
                    mainImage.alt = img.alt;
                }
            }
        }

        // Listen for carousel movements (arrows, drag, programmatic)
        thumbnailSplide.on('moved', function(newIndex) {
            updateMainImage(newIndex);
        });

        // Add click listeners to thumbnail images
        thumbnails.forEach(function(thumb, index) {
            thumb.addEventListener('click', function(e) {
                e.preventDefault();
                e.stopPropagation();
                thumbnailSplide.go(index);
                updateMainImage(index);
            });
        });

        // Initialize and open FSLightbox
        function openLightbox(index) {
            if (typeof FsLightbox !== 'undefined') {
                const lightbox = new FsLightbox();
                lightbox.props.sources = images;
                lightbox.props.type = 'image';
                lightbox.props.slideDistance = 0.3;
                lightbox.open(index);
            }
        }

        // Main image click opens lightbox
        const mainWrapper = document.getElementById('mainImageWrapper');
        if (mainWrapper) {
            mainWrapper.addEventListener('click', function() {
                openLightbox(thumbnailSplide.index);
            });
        }

        // Set initial main image
        updateMainImage(0);
    }
    // ===== END THUMBNAIL CAROUSEL =====

    const homePage = document.getElementById("homepage");
    if (homePage) {
        var heroSlider = new Splide("#hero-slider", {
          type: "loop",
          autoplay: true,
          interval: 5000,
          speed: 1000,
          pauseOnHover: true,
          pauseOnFocus: true,
          arrows: true,
          pagination: false,
          height: "auto",
          cover: true,
          lazyLoad: "nearby",
          breakpoints: {
            768: {
              arrows: true,              
            },
            480: {
              arrows: false,              
            },
          },
        });
        var reviewsSlider = new Splide("#reviews-slider", {
          type: "loop",
          perPage: 1,
          perMove: 1,
          gap: "1em",
          pagination: false,
          arrows: true,
          autoplay: true,
          interval: 5000,
          pauseOnHover: true,
          drag: true,
        });

        var newlyLaunched = new Splide("#newly-launched-slider", {
          type: "loop",
          perPage: 5,
          perMove: 1,
          gap: "10px",
          pagination: false,
          arrows: true,
          drag: true,
          snap: true,
          breakpoints: {
            1200: {
              perPage: 4,
            },
            992: {
              perPage: 3,
            },
            768: {
              perPage: 2,
              gap: "15px",
            },
            576: {
              perPage: 1,
              gap: "10px",
            },
          },
        });

        var bestSellersSlider = new Splide("#best-sellers-slider", {
          type: "loop",
          perPage: 5,
          perMove: 1,
          gap: "10px",
          pagination: false,
          arrows: true,
          drag: true,
          snap: true,
          breakpoints: {
            1200: {
              perPage: 4,
            },
            992: {
              perPage: 3,
            },
            768: {
              perPage: 2,
              gap: "15px",
            },
            576: {
              perPage: 1,
              gap: "10px",
            },
          },
        });


      reviewsSlider.mount();
      newlyLaunched.mount();
      bestSellersSlider.mount();
      heroSlider.mount();
    }   

    const tabButtons = document.querySelectorAll('.spdesc-tab-button');
    const tabContents = document.querySelectorAll('.spdesc-tab-content');

    tabButtons.forEach(button => {
      button.addEventListener('click', function() {
        const targetTab = this.getAttribute('data-tab');
        
       
        tabButtons.forEach(btn => btn.classList.remove('active'));
        tabContents.forEach(content => content.classList.remove('active'));
        
        // Add active class to clicked button and corresponding content
        this.classList.add('active');
        const targetContent = this.nextElementSibling;
        targetContent.classList.add('active');
      });
    });     

     

    const productdetails = document.getElementById("product-details");
    if (productdetails) {
       class Tabcordion {
        constructor(root, { breakpointPx = 1024 } = {}) {
        this.root = root;
        this.nav = root.querySelector(".tcnav-tabs");
        this.tabContent = root.querySelector(".tab-content");
        this.tabs = [...this.nav.querySelectorAll('a[href^="#"]')];
        this.panes = this.tabs.map((a) =>
          root.querySelector(a.getAttribute("href"))
        );

        this._origParent = this.panes.map((p) => p.parentElement);
        this._origNext = this.panes.map((p) => p.nextSibling);

        // Build accordion
        this.acc = document.createElement("div");
        this.acc.className = "tabcordion-acc";
        this.accItems = [];

        this.tabs.forEach((a, i) => {
          const pane = this.panes[i];
          const item = document.createElement("div");
          item.className = "tc-acc-item";

          const btn = document.createElement("button");
          btn.className = "tc-acc-button";
          btn.type = "button";
          const paneId = pane.id || `tc-pane-${i}`;
          pane.id = paneId;
          btn.setAttribute("aria-controls", paneId);
          btn.setAttribute("aria-expanded", pane.classList.contains("active"));
          btn.textContent = a.textContent.trim();

          const panel = document.createElement("div");
          panel.className = "tc-acc-panel";
          panel.id = `acc-${paneId}`;
          panel.setAttribute("role", "region");

          item.appendChild(btn);
          item.appendChild(panel);
          this.acc.appendChild(item);
          this.accItems.push({ item, btn, panel, pane, tab: a });
        });

        // Listeners
        this.nav.addEventListener("click", (e) => {
          const link = e.target.closest('a[href^="#"]');
          if (!link) return;
          e.preventDefault();
          this.activateTab(link.getAttribute("href"));
        });

        this.acc.addEventListener("click", (e) => {
          const btn = e.target.closest(".tc-acc-button");
          if (!btn) return;
          setTimeout(() => {
            console.log(btn, "btn.offsettop", btn.offsetTop);
            const btnPosition =
              btn.getBoundingClientRect().top + window.pageYOffset;
            window.scrollTo({ top: btnPosition, behavior: "smooth" });
          }, 500);

          this.toggleAccordion("#" + btn.getAttribute("aria-controls"));
        });

        this.mq = window.matchMedia(`(min-width: ${breakpointPx}px)`);
        this.mq.addEventListener("change", () => this.layout());

        this.layout();
      }

      activateTab(hash) {
        const target = this.root.querySelector(hash);
        if (!target) return;
        this.tabs.forEach((t) => t.classList.remove("active"));
        this.panes.forEach((p) => p.classList.remove("active"));
        const tab = this.tabs.find((t) => t.getAttribute("href") === hash);
        if (tab) tab.classList.add("active");
        target.classList.add("active");

        // Reflect in accordion
        this.accItems.forEach(({ btn, panel, pane }) => {
          const open = pane === target;
          btn.setAttribute("aria-expanded", open);
          panel.classList.toggle("show", open);
        });
      }

      toggleAccordion(hash) {
        const target = this.root.querySelector(hash);
        if (!target) return;
        const wasOpen = target.classList.contains("active");
        this.panes.forEach((p) => p.classList.remove("active"));
        this.tabs.forEach((t) => t.classList.remove("active"));
        this.accItems.forEach(({ btn, panel }) => {
          btn.setAttribute("aria-expanded", "false");
          panel.classList.remove("show");
        });
        if (!wasOpen) {
          target.classList.add("active");
          const pair = this.accItems.find((x) => x.pane === target);
          if (pair) {
            pair.btn.setAttribute("aria-expanded", "true");
            pair.panel.classList.add("show");
          }
          const tab = this.tabs.find((t) => t.getAttribute("href") === hash);
          if (tab) tab.classList.add("active");
        }
      }

      layout() {
        if (this.mq.matches) this._unmountAccordion();
        else this._mountAccordion();
      }

      _mountAccordion() {
        if (this.acc.parentNode) return;
        this.nav.insertAdjacentElement("afterend", this.acc);
        this.accItems.forEach(({ panel, pane }) => panel.appendChild(pane));
        const activePane =
          this.panes.find((p) => p.classList.contains("active")) ||
          this.panes[0];
        this.toggleAccordion("#" + activePane.id);
      }

      _unmountAccordion() {
        if (!this.acc.parentNode) return;
        this.panes.forEach((pane, i) => {
          const parent = this._origParent[i];
          const next = this._origNext[i];
          if (next && next.parentNode === parent)
            parent.insertBefore(pane, next);
          else parent.appendChild(pane);
        });
        this.acc.remove();
        const activePane =
          this.panes.find((p) => p.classList.contains("active")) ||
          this.panes[0];
        this.activateTab("#" + activePane.id);
      }
    }

    // Initialization function
    function initTabcordion() {
      const root = document.querySelector("#description");
      if (!root) return; // Safety check
      root.classList.remove("no-js");
      new Tabcordion(root, { breakpointPx: 1024 });
      if (typeof relatedProducts !== "undefined") {
        relatedProducts.mount();
      }
    }
    initTabcordion();

    const toggleBtn = document.getElementById("toggleBtn");
    const toggleText = document.getElementById("toggleText");
    const toggleIcon = document.getElementById("toggleIcon");
    const reviewList = document.getElementById("reviewList");

    toggleText.textContent = reviewList.classList.contains("visible") ? "Hide Review" : "Read Reviews";
     toggleBtn.addEventListener("click", () => {
       reviewList.classList.toggle("visible");
       toggleIcon.classList.toggle("rotated");
       toggleText.textContent = reviewList.classList.contains("visible")
         ? "Hide Review"
         : "Read Reviews";
     });

     // Helpful button interactions
     const helpfulBtns = document.querySelectorAll(".sm-review-helpful-btn");
     helpfulBtns.forEach((btn) => {
       btn.addEventListener("click", (e) => {
         e.target.style.transform = "scale(1.3)";
         setTimeout(() => {
           e.target.style.transform = "scale(1)";
         }, 200);
       });
     });     

  }


  

});


// Reusable modal initializer
function initTingleModal(buttonId, contentId, onOpen) {
  const modal = new tingle.modal({
    closeMethods: ['overlay', 'escape', 'button'],
    closeLabel: "Close",
    onOpen: function() {
      // Initialize any interactive elements after modal opens
      if (onOpen) onOpen(modal);
    }
  });
  
  // Bind click to open modal
  document.getElementById(buttonId).addEventListener("click", () => {
    // Load content when modal opens (not on page load)
    modal.setContent(document.getElementById(contentId).innerHTML);
    modal.open();
  });
  
  return modal;
}


const productdetails = document.getElementById("product-details");
if (productdetails) {
  
    var relatedProducts = new Splide("#related-products", {
      type: "loop",
      perPage: 5,
      perMove: 1,
      gap: "10px",
      pagination: false,
      arrows: true,
      drag: true,
      snap: true,
      breakpoints: {
        1200: {
          perPage: 4,
        },
        992: {
          perPage: 3,
        },
        768: {
          perPage: 2,
          gap: "15px",
        },
        576: {
          perPage: 1,
          gap: "10px",
        },
      },
    });


    // Interactive Rating Handler
    function initInteractiveRating(ratingElement) {
      const stars = ratingElement.querySelectorAll('.smp-rating__star');
      let currentRating = 0;
      
      stars.forEach(star => {
        star.addEventListener('click', function() {
          currentRating = parseInt(this.dataset.value);
          ratingElement.dataset.rating = currentRating;
          updateStars(currentRating);
        });
        
        star.addEventListener('mouseenter', function() {
          const hoverValue = parseInt(this.dataset.value);
          updateStars(hoverValue);
        });
      });
      
      ratingElement.querySelector('.smp-rating__stars').addEventListener('mouseleave', function() {
        updateStars(currentRating);
      });
      
      function updateStars(rating) {
        stars.forEach((star, index) => {
          if (index < rating) {
            star.setAttribute('fill', '#f9a61a');
          } else {
            star.setAttribute('fill', '#ddd');
          }
        });
      }
    }

    // Initialize modals with callbacks
    const reviewModal = initTingleModal("write-review", "reviewModal", function(modal) {
      // Find rating element inside the modal and initialize it
      const ratingElement = modal.modalBox.querySelector('.smp-rating');
      if (ratingElement) {
        initInteractiveRating(ratingElement);
      }
    });

    const qnaModal = initTingleModal("ask-question", "qnaModal");
    const proAdded = initTingleModal("addto-basket", "proaddedModal");

    // Initialize interactive rating
    const interactiveRating = document.getElementById('interactiveRating');
    if (interactiveRating) {
      initInteractiveRating(interactiveRating);
    }
    // Function to get rating value (for use in your application)
    function getRating(ratingElement) {
      return parseFloat(ratingElement.dataset.rating) || 0;
    }
    // Interactive Rating Handler
   
}


   

    function getTemplateHtml(selector) {
      const tpl = document.querySelector(selector);
      if (!tpl || tpl.tagName !== 'TEMPLATE') return null;
      const frag = tpl.content.cloneNode(true);
      const div = document.createElement('div');
      div.appendChild(frag);
      return div.innerHTML;
    }



  const productlisting = document.getElementById("listing");  
  if (productlisting){
     const modal = new tingle.modal({
       closeMethods: ["overlay", "button", "escape"],
       closeLabel: "Close dialog",
       // don't touch footer unless you actually add one
       onClose() {
         modal.setContent("");
       },
     });
     
    document.addEventListener('click', (e) => {
      const btn = e.target.closest('.js-open-modal');
      if (!btn || btn.disabled || btn.getAttribute('aria-disabled') === 'true') return;
      e.preventDefault();

      const sel = btn.getAttribute('data-modal');
      const html = sel ? getTemplateHtml(sel) : null;
      if (!html) { console.warn('Missing/invalid modal template:', sel); return; }

      modal.setContent(html);
      modal.open();
    });
  }

  function increaseQuantity() {
          const input = document.getElementById('quantity');
          input.value = parseInt(input.value) + 1;
      }

  function decreaseQuantity() {
      const input = document.getElementById('quantity');
      if (parseInt(input.value) > 1) {
          input.value = parseInt(input.value) - 1;
      }
  }

  function increaseQuantity() {
          const input = document.getElementById('basket-quantity');
          input.value = parseInt(input.value) + 1;
      }

  function decreaseQuantity() {
      const input = document.getElementById('basket-quantity');
      if (parseInt(input.value) > 1) {
          input.value = parseInt(input.value) - 1;
      }
  }
  

  
 
   // Global scroll function
    function scrollToDiv(target, offset = 0) {
      const el = typeof target === 'string' ? document.querySelector(target) : target;
      
      if (!el) {
        console.warn('Element not found:', target);
        return;
      }
      
      const pos = el.getBoundingClientRect().top + window.pageYOffset - offset;
      
      window.scrollTo({
        top: pos,
        behavior: 'smooth'
      });
    } 
    
    const myAccount = document.querySelector(".login-bg");
     if (myAccount) {
       /*password-hide*/
       const passwordInput = document.getElementById("password");
       const passwordToggle = document.getElementById("password-toggle");

       passwordToggle.addEventListener("click", function () {
         if (passwordInput.type === "password") {
           passwordInput.type = "text";
           passwordToggle.classList.add("active");
         } else {
           passwordInput.type = "password";
           passwordToggle.classList.remove("active");
         }
       });

       const reGister = initTingleModal("create-account", "registerModal");
       const forGotpass = initTingleModal("forGotpass", "forgotpassModal");

     }

    const myDashboard = document.querySelector(".myacc-bg");
     if (myDashboard) {
       
       const chgBill = initTingleModal("chgBill-add", "chgBillModal");
       const chgShip = initTingleModal("chgShip-add", "chgShipModal");
       const accPass = initTingleModal("accPass-chg", "accPssModal");

     }