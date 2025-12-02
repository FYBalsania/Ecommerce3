function debounce(fn, threshold) {
    var timeout;
    threshold = threshold || 100;
    return function debounced() {
        clearTimeout(timeout);
        var args = arguments;
        var _this = this;
        function delayed() {
            fn.apply(_this, args);
        }
        timeout = setTimeout(delayed, threshold);
    };
}


function throttle(fn, limit) {
    let lastFn;
    let lastRan;
    return function (...args) {
        if (!lastRan) {
            fn(...args);
            lastRan = Date.now();
        } else {
            clearTimeout(lastFn);
            lastFn = setTimeout(function () {
                if (Date.now() - lastRan >= limit) {
                    fn(...args);
                    lastRan = Date.now();
                }
            }, limit - (Date.now() - lastRan));
        }
    };
}

// new js for Buildworld //
const productListing = document.getElementById("listing");
function deliverySelection() {
    const filterButton = document.querySelector(".filter_button");
    if (filterButton) {
        filterButton.removeEventListener("click", handleFilterClick);
        filterButton.addEventListener("click", handleFilterClick);
    }
}

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
        filterButton.innerHTML = `<img src="https://files.buildworld.co.uk/img/bw-filter-icon.svg" alt="Filter"> Close`;
        filterButton.disabled = true;
        setTimeout(() => {
            filterButton.disabled = false;
        }, 500);
        document.body.classList.toggle("overflow-hidden");        
    } else {
        setTimeout(() => {
          document.body.removeChild(overlay);
          filterButton.disabled = false;
        }, 500);
        filterButton.disabled = true;
        overlay.style.opacity = 0;
        overlay.style.visibility = "hidden";
        filterButton.classList.remove("active");
        filters.classList.remove("active");
        filterButton.innerHTML = `<img src="https://files.buildworld.co.uk/img/bw-filter-icon.svg" alt="Filter"> Filters`;
        document.body.classList.remove("overflow-hidden");
    }

    if (overlay) {
        overlay.addEventListener("click", () => {
            filterButton.classList.remove("active");
            filterButton.innerHTML = `<img src="https://files.buildworld.co.uk/img/bw-filter-icon.svg" alt="Filter"> Filters`;
            filters.classList.remove("active");
            document.body.classList.remove("overflow-hidden");
            filterButton.disabled = true;
            setTimeout(() => {
                overlay.style.opacity = 0;
                overlay.style.visibility = "hidden";
                filterButton.disabled = false;
                setTimeout(() => {
                    document.body.removeChild(overlay);
                }, 600);
            }, 500);
        });
    }    
    if (accordion) {
        const accordionItems = accordion.querySelectorAll(".accordion-item");
        accordionItems.forEach(item => {
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
                        showMore.style.cssText = "display:flex; opacity:1; visibility:visible";                        
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
                    console.log(target.nextElementSibling)
                    target.nextElementSibling.style.height = target.nextElementSibling.offsetHeight + "px";
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
    accordionItem.forEach(item => {        
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
    let target = document.getElementById(element.getAttribute('aria-controls'));    
    if (target) {
        element.setAttribute('aria-expanded', show);
        target.setAttribute('aria-hidden', !show);
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
    accordionContainer.addEventListener('click', function (event) {
        let target = event.target;
       

        if (target.closest) {
            target = target.closest('[class*="accordion-title"]');
        }

        if (target) {
            let isTargetOpen = target.getAttribute('aria-expanded') === 'true';
            closeAllPanels();
            InsideAccordionToggle(target, !isTargetOpen);
        }
    });
}
let insideAccordion = document.querySelectorAll(".accordion");



// Smooth Scroll Code getting used in BuildworkUK-v1.js
function smoothScrollToTarget(duration, targetPosition) {
    const start = window.scrollY;
    const startTime = "now" in window.performance ? performance.now() : new Date().getTime();
    function scroll() {
        const now = "now" in window.performance ? performance.now() : new Date().getTime();
        const elapsed = now - startTime;
        const progress = Math.min(elapsed / duration, 1);
        window.scrollTo(0, start + (targetPosition - start) * progress);
        if (progress < 1) {
            requestAnimationFrame(scroll);
        }
    }
    requestAnimationFrame(scroll);
}



document.addEventListener('DOMContentLoaded', function () {
    const sIcon = document.querySelector(".search_icon");
    const mSearch = document.querySelector(".mobile_tab_search");
    if (sIcon) {
        sIcon.addEventListener("click", function (e) {
            e.preventDefault();
            window.scrollTo(0, 0);
            if (getComputedStyle(mSearch).display === "none") {
                mSearch.style.display = "block";
                setTimeout(() => {
                    mSearch.style.transition = "all 0.5s ease";
                    mSearch.style.width = "100%";
                    mSearch.style.height = "45px";
                }, 100);
            } else {
                setTimeout(function () {
                    mSearch.style.display = "none";
                }, 400);
                mSearch.style.transition = "all 0.5s ease";
                mSearch.style.width = "0";
                mSearch.style.height = "0";
            }
        });
    }



    let splideObject = { type: "loop", autoplay: "true" };
    let splideObject2 = {
        perPage: 3,
        perMove: 1,
        gap: "1rem",
        pagination: false,
        type: "slide",
        breakpoints: {
            1023: { perPage: 2 },
            640: { perPage: 2 },
            420: { perPage: 1 },
            focus: "center",
            gap: "2em",
            updateOnMove: true,
            pagination: false,
        },
    };


    let header = {
        perPage: 5,
        perMove: 0,
        gap: "1rem",
        pagination: false,
        type: "slide",
        arrows: false,
        drag:false,
        destroy: true,
        breakpoints: {
            1023: { perPage: 1, autoplay: true, type: "loop", destroy: false, focus: 'center' }
        },
    };

    let bottomSplide = {
        perPage: 5,
        perMove: 0,
        gap: "1rem",
        pagination: false,
        type: "slide",
        arrows: false,
        destroy: true,
        drag:false,
        cover:false,
        breakpoints: {
            1023: { perPage: 2, perMove:1, destroy: false, autoplay: false, arrows:true },
            640: { perPage: 1, destroy: false, arrows:true, autoplay: false, focus:'center' }
        }
    }

    let blogSlider = {
        perPage: 3,
        perMove: 0,
        gap: "1rem",
        pagination: false,
        type: "slide",
        arrows: false,
        destroy: true,
        autoplay: false,
        drag:false,
        breakpoints: {
            1023: { perPage: 2, perMove:1, destroy: false, arrows: true },
            640: { perPage: 1, perMove:1, destroy: false, arrows: true }
        }
    };

    let thumnailSlideObject = {
        gap: 10,
        rewind: true,
        pagination: false,
        isNavigation: true,
        cover: false
    };

    let productForObject = {
        type: "fade",
        rewind: true,
        pagination: false,
        arrows: false,
        cover: false
    };

    let relatedProducts = {
        perPage: 4,
        perMove: 1,
        gap: "1rem",
        pagination: false,
        type: "slide",
        destroy: true,
        breakpoints: {
            1023: { perPage: 2, destroy: false },
            640: { perPage: 2, destroy: false },
            420: { perPage: 1, destroy: false },
            focus: "center",
            gap: "2em",
            updateOnMove: true,
            pagination: false,
        },
    };


    let homeSlider = document.getElementById("homeslider");
    let tdayPick = document.getElementById("tdaypick");
    let relatedProductsDiv = document.getElementById("frequently_bought");
    let related_items = document.getElementById("related_items");
    let bottomSlide = document.querySelector(".choose-main_bg");
    let fromTheBlog = document.querySelector(".relarticles");
    if (bottomSlide) {
        new Splide(bottomSlide, bottomSplide).mount();
    }
    if (homeSlider) {
        new Splide(homeSlider, splideObject).mount();
    }
    if (tdayPick) {
        new Splide(tdayPick, splideObject2).mount();
    }
    
    if (fromTheBlog && productListing) {
      new Splide(fromTheBlog, blogSlider).mount();
    }

    function findSlideIndexByDataItemIndex(dataItemIndex) {
        const slides = document.querySelectorAll(".product-image .splide__slide");
        for (let i = 0; i < slides.length; i++) {
            if (slides[i].getAttribute("data-item-index") === dataItemIndex.toString()) {
                return i;
            }
        }
        return -1;
    }

    let sliderDiv = document.getElementById("Sliderdiv");
    let thumnailSlide = document.getElementById("thumnailSlide");
    if (thumnailSlide) {
        let thumbNailImg = thumnailSlide.querySelectorAll("img");
        if ((thumbNailImg.length > 1) && (sliderDiv)) {
            let main = new Splide(sliderDiv, productForObject)
            let thumb = new Splide(thumnailSlide, thumnailSlideObject);
            thumb.mount();
            main.sync(thumb);
            main.mount();

            let template6 = parseInt(document.getElementById("hdnProductTemplate").value);
            if (template6 === 6) {
                let labelSlider = document.querySelectorAll(".radiogroup label");
                labelSlider.forEach(label => {
                    label.addEventListener("click", function (e) {
                        let target = parseInt(e.currentTarget.dataset.slide);
                        const slideIndex = findSlideIndexByDataItemIndex(target);
                        if (slideIndex !== -1 && main.index !== slideIndex) {
                            main.go(slideIndex);
                        }
                    });
                });
            }
        } else {
            sliderDiv.style.visibility = "visible";
            thumnailSlide.style.visibility = "visible";
        }
    }

   
    
    if (relatedProductsDiv) {
        new Splide(relatedProductsDiv, relatedProducts).mount();
    }
    if (related_items) {
        new Splide(related_items, relatedProducts).mount();
    }
    function closeSideBar(side_open) {
        side_open.classList.remove("delivery_active");
        side_open.classList.add("delivery_close");
        side_open.addEventListener("animationend",function () {
                side_open.style.display = "none";
                side_open.classList.remove("delivery_close");
                let overlay = document.querySelector(".overlay");
                if (overlay) {
                    setTimeout(() => {
                    document.body.removeChild(overlay);    
                    }, 300);
                    overlay.style.opacity = 0;
                }
                document.body.classList.remove("overflow-hidden");
            },
            { once: true }
        );
    }
    function openSideBar(side_open) {
        const closeBtn = side_open.querySelector(".close-button");
        document.body.classList.add("overflow-hidden");
        side_open.style.display = "flex";
        side_open.classList.add("delivery_active");
        const overlay = document.createElement("div");
        overlay.classList.add("add_overlay", "overlay");
        side_open.classList.add("delivery_active");
        document.body.appendChild(overlay);
        overlay.addEventListener("click", function (e) {
            e.preventDefault();
            closeSideBar(side_open);
        });
        closeBtn.addEventListener("click", function (e) {
            e.preventDefault();
            closeSideBar(side_open);
        });
    }
    if (productListing) {
        let accordions = document.querySelectorAll(".accordion");
        accordions.forEach(accordion => setupAccordion(accordion));
        allAccordions();
        deliverySelection();
    }


    function loadVideo(videoId, container) {
        const iframe = document.createElement("iframe");
        iframe.width = "100%";
        iframe.height = "100%";
        iframe.src = `https://www.youtube.com/embed/${videoId}?autoplay=1`;
        iframe.frameBorder = "0";
        iframe.allow = "accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture";
        iframe.allowFullscreen = true;
        container.innerHTML = "";
        container.appendChild(iframe);
    }
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const videoContainer = entry.target;
                const videoId = videoContainer.getAttribute("data-video-id");
                 videoContainer.style.backgroundImage = `url(https://img.youtube.com/vi/${videoId}/hqdefault.jpg)`;
                 videoContainer.style.backgroundSize = "cover";
                 videoContainer.style.backgroundPosition = "center";
                // loadVideo(videoId, videoContainer);
                // observer.unobserve(videoContainer);
                // const playButton = videoContainer.querySelector(".play-button");
                // if (playButton) {
                //   playButton.addEventListener("click", (e) => {
                //     e.preventDefault();
                //     loadVideo(videoId, videoContainer);
                //   });
                // }
                if (!videoContainer.querySelector(".playButton")) {
                  const playButton = document.createElement("div");
                  playButton.classList.add("playButton");
                  playButton.textContent = "►";

                  videoContainer.appendChild(playButton);

                  playButton.addEventListener("click", (e) => {
                    e.preventDefault();
                    loadVideo(videoId, videoContainer);
                  });
                }
                observer.unobserve(videoContainer); 
            }
        });
    },
        {
            root: null,
            threshold: 0.1,
        }
    );
    
    const productDetails = document.getElementById("product_details");
    if (productDetails) {
        insideAccordion.forEach(setupInsideAccordion);        
        const deliveryDetails = document.getElementById("del-return");
        const question_faq = document.getElementById("returnsFaq");
        const returnBtn = document.getElementById("returnBtn");
        const suiTable = document.getElementById("suiTable");
        const deliveryBtn = document.getElementById("deliveryBtn");
        const freeBtn = document.getElementById("freeBtn");
        const add_ons = document.querySelector(".add_ons");
        const sectionAddOns = document.querySelector(".add_ons_options");
        const returns = document.getElementById("returns");
        const review_link = document.querySelector(".review_link");
        const readTech = document.querySelector(".readTech");
        const star_rating = document.querySelector(".star-rating");
        const videoContainer = document.getElementById("video_frame");
        
        if (videoContainer) {
            document.querySelectorAll(".video").forEach(video => {
                observer.observe(video);
            });
        }       
        if (star_rating) {
            let ratingThanks = document.getElementById("rating_thanks");
            let ratingThanksHTML = ratingThanks.innerHTML;
            let allInputs = star_rating.querySelectorAll("input");
            allInputs.forEach(input => {
                input.addEventListener("change", function (e) {
                    let rating = parseInt(e.target.value);
                    let content;
                    if (rating === 1 || rating === 2) {
                        content = `<span>We apologize for the inconvenience you've experienced.</span><p class="lead">Your feedback is important to us, and we are committed to improving our services to better meet your expectations.</p>`;
                    } else {
                        content = `<span>Thank you for rating us ${rating} stars</span><p class="lead">We will soon verify the details and add your rating appropriately.</p>`;
                    }

                    let ratingThanks = new tingle.modal({
                        footer: false,
                        stickyFooter: false,
                        closeLabel: "Close",
                        cssClass: ["custom-modal-class"],
                        onClose: function () {
                            input.checked = false;
                        },
                    });
                    ratingThanks.setContent(ratingThanksHTML);
                    ratingThanks.open()
                    document.querySelector(".tingle-modal.tingle-modal--visible .pass-form_tit-container").innerHTML = content;
                });
            });
        }
        if (returns) {
            returns.addEventListener("click", function (e) {
                e.preventDefault();
                openSideBar(question_faq);
            });
        }
        if (returnBtn) {
            returnBtn.addEventListener("click", function (e) {
                e.preventDefault();
                openSideBar(returnModal);
            });
        }
        if (suiTable) {
            suiTable.addEventListener("click", function (e) {
                e.preventDefault();
                openSideBar(suitableModal);
            });
        }

        if (freeBtn) {
            freeBtn.addEventListener("click", function (e) {
                e.preventDefault();
                openSideBar(deliveryDetails);
            });
        }
        if (deliveryBtn) {
            deliveryBtn.addEventListener("click", function (e) {
                e.preventDefault();
                openSideBar(deliveryDetails);
            });
        }
        if (add_ons) {
            add_ons.addEventListener("click", function (e) {
                e.preventDefault();
                smoothScrollToTarget(500, sectionAddOns.offsetTop);
            });
        }
        if (review_link) {
            review_link.addEventListener("click", function (e) {
                e.preventDefault();
                smoothScrollToTarget(500, document.querySelector("section.rating").offsetTop);
            });
        } 
        if (readTech) {
            readTech.addEventListener("click", function (e) {
                e.preventDefault();
                smoothScrollToTarget(500, document.querySelector("div.readTechcont").offsetTop);
            });
        }        
    }


    const loginPage = document.getElementById("formHolder");
    const forgotModal = document.getElementById("forgotpasswordform");
    const registerForm = document.getElementById("registerForm");
    if (loginPage) {
        let forgotPassword = new tingle.modal({
            onClose: function () {
                forgotModal.style.display = "none";
            },
            onOpen: function () {
                loadRecaptcha();
            },
            cssClass: ["small"],
        });
        const forgotPasswordBtn = document.querySelector(".for_pass");
        if (forgotPasswordBtn) {
            forgotPasswordBtn.addEventListener("click", function (e) {
                e.preventDefault();
                forgotModal.style.display = "block";
                forgotPassword.setContent(forgotModal);
                forgotPassword.open();
            });
        }

        let registerModal = new tingle.modal({
            onClose: function () {
                registerForm.style.display = "none";
            },
            onOpen: function () {
                loadRecaptcha();
            }

        });
        const registerButton = document.querySelector(".account_create");
        if (registerButton) {
            registerButton.addEventListener("click", function (e) {
                e.preventDefault();
                registerForm.style.display = "grid";
                registerModal.setContent(registerForm);
                registerModal.open();
            });
        }

        if (registerForm) {
            const validator = new JustValidate('#registerForm', { validateBeforeSubmitting: true });
            validator
                .addField('#rpass', [{ rule: 'required' }])
                .addField('#cpass', [{ rule: 'required', }, {
                    validator: (value, fields) => {
                        if (fields['#rpass'] && fields['#rpass'].elem) {
                            const repeatPasswordValue = fields['#rpass'].elem.value;
                            return value === repeatPasswordValue;
                        }
                        return true;
                    },
                    errorMessage: 'Passwords should be the same',
                },
                ])
                .addField('#rfname', [{ rule: 'required' }])
                .addField('#rlname', [{ rule: 'required' }])
                .addField('#rcontactno', [{ rule: 'required' }, { rule: 'number' }])
                .addField('#location', [{ rule: 'required' }])
                .addField('#rsignupagree', [{ rule: 'required' }])
                .addField('#rmail', [{ rule: 'required', }, { rule: 'email' }])    
              document.querySelectorAll("#registerForm input").forEach(input => {
                if (input.type === "checkbox") return;
                if (input.type === "submit") return;               
                  input.addEventListener("blur", () => {                  
                    const fieldSelector = `#${input.getAttribute('id')}`;                       
                    validator.revalidateField(fieldSelector); 
                  });
              });  
        }

        const forgottonPasswordForm = document.getElementById("forgotpasswordform");
        if (forgottonPasswordForm) {
            const forgottonPassword = new JustValidate("#forgotpasswordform", { validateBeforeSubmitting: true });
            forgottonPassword
                .addField("#login-forgotten-password", [{ rule: "required" }, { rule: "email" }]);
        }

        const signInFormElem = document.getElementById("signInForm");
        if (signInFormElem) {
          const signInForm = new JustValidate('#signInForm', { validateBeforeSubmitting: true });
            signInForm
                .addField("#mail", [{ rule: "required" }, { rule: "email" }])
                .addField("#pass", [{ rule: "required" }]);       
           
            document.querySelectorAll("#signInForm input").forEach(input => { 
                if (input.id === "signin") return;
                  input.addEventListener("blur", () => {
                    const fieldSelector = `#${input.getAttribute("id")}`;                    
                    signInForm.revalidateField(fieldSelector);
                  });
            });
        }
    }

    
    const accountPage = document.getElementById("my-account");
    if (accountPage) {
        const changePasswordModal = new tingle.modal({ cssClass: ["small"] });
        const changePassword = document.getElementById("change-password");
        const changePasswordForm = document.querySelector(".forgot-pass-form");
        changePassword.addEventListener("click", function (e) {
            e.preventDefault();                        
            changePasswordModal.setContent(changePasswordForm);
            changePasswordModal.open();
        });

        const updateBillingForm = document.getElementById("chg_bill_add");
        const updatebillingModal = new tingle.modal({
            onOpen: function () {
             updateBillingForm.style.display = "block";
            },
            onClose: function () {
               window.location.reload();
            }
        });
        const updateBillingBtn = document.getElementById("chg_bill_add_btn");        
        updateBillingBtn.addEventListener("click", function (e) {
            e.preventDefault();            
            updatebillingModal.setContent(updateBillingForm);
            updatebillingModal.open();
        });

        const shippingAddressForm = document.getElementById("chg_ship_add_frm");
        const shippingAddressModal = new tingle.modal({
            onOpen: function () {
                shippingAddressForm.style.display = "block";
            },
            onClose: function () {
                window.location.reload();
            }
        });
        const shippingAddressBtn = document.getElementById("chg_ship_add");
        shippingAddressBtn.addEventListener("click", function (e) {
            e.preventDefault();
            shippingAddressModal.setContent(shippingAddressForm);
            shippingAddressModal.open();
        });
        
    }  


    const blogPages = document.getElementById("blogContent");
    if (blogPages) {
        const categoryBtn = document.querySelector(".arc-but-1");
        const blogSideBar = document.getElementById("blogCats");
        categoryBtn.addEventListener("click", function (e) {
            e.preventDefault();
            openSideBar(blogSideBar);
        });

        const stepsScroll = document.querySelector(".steps-scroll");
        if (stepsScroll) {
            const anchorTag = stepsScroll.querySelectorAll("a");
            anchorTag.forEach(tag => {
                tag.addEventListener("click", function (e) {
                    e.preventDefault();
                    let target = e.target.getAttribute("href");
                    let targetElement = document.querySelector(target);
                    smoothScrollToTarget(500, targetElement.offsetTop);
                });
            });
        }

    }

    window.onloadCallback = function () {
        document.querySelectorAll('.recaptcha-placeholder').forEach(function (element) {
            if (!element.dataset.rendered) {
                grecaptcha.render(element, {
                    'sitekey': '6LfcUScUAAAAAMHXt3ixDrrBaG42XS_fnyo3lJ50', //live
                    //'sitekey': '6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI', //test				  
                });
                element.dataset.rendered = 'true';
            }
        });
    };
    function loadRecaptcha() {
        if (!window.grecaptcha) {
            let script = document.createElement("script");
            script.src = "https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit";
            document.body.appendChild(script);
        } else {
            window.onloadCallback();
        }
    }   

    const termBtn = document.getElementById("termBtn");
    if (termBtn)
        termBtn.addEventListener("click", function (e) {
            e.preventDefault();
            openSideBar(termModal);
        });

        
    let newModal, modal;
    function openModal(modalContentId) {
        if (!modal) {
            modal = new tingle.modal();
            let content = document.getElementById(modalContentId).innerHTML;
            modal.setContent(content);
            modal.open();
            newModal = modal;

        } else {
            modal.open();
        }
    }
    modal = newModal;

    let openModals = document.querySelectorAll("button.open-modal[data-id]");
    openModals.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            let modalId = e.target.getAttribute("data-id");
            openModal(modalId);
        });
    });


    let smodal;
    function smModals(modalContentId) {      
      if (!smodal) {        
        smodal = new tingle.modal({ cssClass: ["small"] });
      }      
      let content = document.getElementById(modalContentId).innerHTML;
      smodal.setContent(content);
      smodal.open();
    }    
    let smModalButtons = document.querySelectorAll("button.small[data-id]");
    smModalButtons.forEach(button => {
      button.addEventListener("click", function (e) {
        e.preventDefault();
        let modalId = e.target.getAttribute("data-id");
        smModals(modalId);
      });
    });

    // Contact us
    const contactPage = document.getElementById("contactPage");
    if (contactPage) {
        let collapsingTabs = document.getElementById("collapsing-tabs");
        let collapsingLi = collapsingTabs.querySelectorAll("li a");
        let collapsingContent = document.querySelector(".tabs-content");
        let collapsingDiv = collapsingContent.querySelectorAll("div.tabs-panel");
        collapsingLi.forEach((li, index) => {
            li.addEventListener("click", function (e) {
                e.preventDefault();
                collapsingLi.forEach(li => {
                    li.classList.remove("is-active");
                    li.setAttribute("aria-selected", "false");
                });
                li.classList.add("is-active");
                li.setAttribute("aria-selected", "true");
                collapsingDiv.forEach((div) => {
                    div.classList.remove("is-active");
                });
                collapsingDiv[index].classList.add("is-active");
            });
        });

        const generalEnquiryForm = document.getElementById("generalenquiryForm");
        if (generalEnquiryForm) {
            const enquiryForm = new JustValidate("#generalenquiryForm", { validateBeforeSubmitting: true });
            enquiryForm
              .addField("#cfname", [{ rule: "required" }])
              .addField("#cemail", [{ rule: "required" }, { rule: "email" }])
              .addField("#ccno", [{ rule: "required" }, { rule: "number" }])
              .addField("#cFeedback", [{ rule: "required" }]);
            document.querySelectorAll("#generalenquiryForm input, #generalenquiryForm textarea").forEach(input => {
                if (input.type === "checkbox") return;
                input.addEventListener("blur", () => {
                    const fieldSelector = `#${input.getAttribute("id")}`;
                    enquiryForm.revalidateField(fieldSelector);
                });
            });
        }

        const supportForm = document.getElementById("supportForm");
        if (supportForm) {
            const supportFormValidation = new JustValidate("#supportForm", { validateBeforeSubmitting: true });
            supportFormValidation
              .addField("#bwOrderSKU", [{ rule: "required" }])
              .addField("#bwOrderemail", [{ rule: "required" }, { rule: "email" }])
              .addField("#cquestion", [{ rule: "required" }]);
            document.querySelectorAll("#supportForm input, #supportForm textarea").forEach(input => {
                if (input.id === "FileUpload1") return;
                if (input.type === "checkbox") return;
                input.addEventListener("blur", () => {
                    const fieldSelector = `#${input.getAttribute("id")}`;
                    supportFormValidation.revalidateField(fieldSelector);
                });
            });
        }
        
    }

    // terms and condition page
    const terms = document.getElementById("terms");
    if (terms) {
        let eventBound = false;
        function handleTermsClick() {
            const termsContent = document.querySelector(".show_for_small.helpHub");
            const termsUL = document.querySelector(".helphub-nav .ac-nav-links");
            const termsLi = termsUL.querySelectorAll("li");
            if (Modernizr.mq('(max-width: 1023px)')) {
                termsContent.addEventListener("click", function (e) {
                    e.preventDefault();
                    this.classList.toggle("active");

                    termsUL.classList.toggle("is-activated");
                    termsLi.forEach(li => {
                        if (li.parentElement.classList.contains("is-activated")) {
                            li.classList.add("is-active");
                            setTimeout(() => {
                                li.classList.add("animate-show");
                            }, 500);
                        } else {
                            li.classList.remove("animate-show");
                            setTimeout(() => {
                                li.classList.remove("is-active");
                            }, 500);
                        }
                    });
                });
            }
        }
        function checkViewport() {
            if (Modernizr.mq("(max-width: 1023px)")) {
                if (!eventBound) {
                    handleTermsClick();
                    eventBound = true;
                }
            } else {
                const termsContent = document.querySelector(".ac-nav-links");
                if (eventBound && termsContent) {
                    termsContent.replaceWith(termsContent.cloneNode(true)); 
                    eventBound = false;
                }
            }
        }
        checkViewport();
        window.addEventListener("resize", checkViewport);
    }

    const hubPages = document.querySelector(".hub_pages");
    if (hubPages) {
        const helpfulCtaYes = document.querySelector(".helpful-cta-yes");
        const helpfulCtaNo = document.querySelector(".helpful-cta-no");
        const helpHubCta = document.querySelector(".help-hub-cta");
        const helpHubForm = document.querySelector(".helphub-form");
        if (helpfulCtaYes) {
            helpfulCtaYes.addEventListener("click", function (e) {
                e.preventDefault();
                if (!document.querySelector(".thanks")) {
                    const thanks = document.createElement("p");
                    thanks.style.cssText = "border:1px solid #360a5b; color:#360a5b; padding:10px; margin-bottom:10px";
                    thanks.classList.add("thanks");
                    thanks.textContent = "Thank you for your feedback!";
                    helpHubCta.prepend(thanks);
                }
                if (helpHubForm.classList.contains("show-form")) {
                    helpHubForm.classList.remove("show-form");
                }

            });
        }
        if (helpfulCtaNo) {
            helpfulCtaNo.addEventListener("click", openEmailForm);
        }
        function openEmailForm(evt) {
            evt.preventDefault();
            const form = document.querySelector(".helphub-form");
            const thanks = document.querySelector(".thanks");
            if (thanks) {
                thanks.remove();
            }
            if (form.classList.contains("show-form")) {
                form.classList.remove("show-form");
                setTimeout(() => (form.style.display = "none"), 500);
            } else {
                form.style.display = "flex";
                setTimeout(() => form.classList.add("show-form"), 10);
            }
        }

        const dropArea = document.getElementById('drop-area');
        const hhgallery = document.getElementById('hhgallery');
        let processedFiles = new Set();
        if (dropArea) {
            ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(event => {
                dropArea.addEventListener(event, preventDefaults, false);
            });

            function preventDefaults(event) {
                event.preventDefault();
                event.stopPropagation();
            }


            ['dragenter', 'dragover'].forEach(event => {
                dropArea.addEventListener(event, () => dropArea.classList.add('highlight'), false);
            });

            ['dragleave', 'drop'].forEach(event => {
                dropArea.addEventListener(event, () => dropArea.classList.remove('highlight'), false);
            });


            dropArea.addEventListener('drop', handleDrop, false);

            function handleDrop(event) {
                let files = event.dataTransfer.files;
                handleFiles(files);
            }

            function handleFiles(files) {
                
                [...files].forEach(file => {
                    if (!processedFiles.has(file.name)) { 
                        processedFiles.add(file.name);    
                        validateAndUploadFile(file);
                    }
                });
            }


            function validateAndUploadFile(file) {
                const validTypes = ['image/jpeg', 'image/png', 'image/gif', 'application/pdf'];
                if (validTypes.includes(file.type)) {
                    uploadFile(file);
                } else {
                    showErrorhh('Only image and PDF files are allowed.');
                }
            }


            function uploadFile(file) {
                let reader = new FileReader();

                reader.onloadend = function() {
                    let div = document.createElement('div');
                    div.classList.add('hhthumbnail');

                    if (file.type === 'application/pdf') {
                        let pdfDiv = document.createElement('div');
                        pdfDiv.classList.add('pdf-icon');
                        pdfDiv.innerHTML = "<span></span>";
                        div.appendChild(pdfDiv);
                    } else {
                        let img = document.createElement('img');
                        img.src = reader.result;
                        div.appendChild(img);
                    }

                    let closeButton = document.createElement('button');
                    closeButton.classList.add('close');
                    closeButton.innerHTML = '&#10006;';
                    closeButton.onclick = function() {
                        hhgallery.removeChild(div);
                        processedFiles.delete(file.name); 
                    };

                    div.appendChild(closeButton);
                    hhgallery.appendChild(div);
                };

                reader.readAsDataURL(file);
            }


            function showErrorhh(message) {
                const pTag = dropArea.querySelector('p');
                const originalText = pTag.innerText;

                dropArea.classList.add('error');
                pTag.innerText = message;

                setTimeout(() => {
                    pTag.innerText = originalText;
                    dropArea.classList.remove('error');
                }, 3000);
            }


            document.getElementById('fileElem').addEventListener('change', function(event) {
                let files = this.files;
                if (files.length > 0) {
                    handleFiles(files);
                }
            });
        }
        const emailBtn = document.getElementById("email_btn");
        if (emailBtn) {
            emailBtn.addEventListener("click", openEmailForm);
        }
    }

});






/*! lazysizes - v5.3.2 */
!function (e) { var t = function (u, D, f) { "use strict"; var k, H; if (function () { var e; var t = { lazyClass: "lazyload", loadedClass: "lazyloaded", loadingClass: "lazyloading", preloadClass: "lazypreload", errorClass: "lazyerror", autosizesClass: "lazyautosizes", fastLoadedClass: "ls-is-cached", iframeLoadMode: 0, srcAttr: "data-src", srcsetAttr: "data-srcset", sizesAttr: "data-sizes", minSize: 40, customMedia: {}, init: true, expFactor: 1.5, hFac: .8, loadMode: 2, loadHidden: true, ricTimeout: 0, throttleDelay: 125 }; H = u.lazySizesConfig || u.lazysizesConfig || {}; for (e in t) { if (!(e in H)) { H[e] = t[e] } } }(), !D || !D.getElementsByClassName) { return { init: function () { }, cfg: H, noSupport: true } } var O = D.documentElement, i = u.HTMLPictureElement, P = "addEventListener", $ = "getAttribute", q = u[P].bind(u), I = u.setTimeout, U = u.requestAnimationFrame || I, o = u.requestIdleCallback, j = /^picture$/i, r = ["load", "error", "lazyincluded", "_lazyloaded"], a = {}, G = Array.prototype.forEach, J = function (e, t) { if (!a[t]) { a[t] = new RegExp("(\\s|^)" + t + "(\\s|$)") } return a[t].test(e[$]("class") || "") && a[t] }, K = function (e, t) { if (!J(e, t)) { e.setAttribute("class", (e[$]("class") || "").trim() + " " + t) } }, Q = function (e, t) { var a; if (a = J(e, t)) { e.setAttribute("class", (e[$]("class") || "").replace(a, " ")) } }, V = function (t, a, e) { var i = e ? P : "removeEventListener"; if (e) { V(t, a) } r.forEach(function (e) { t[i](e, a) }) }, X = function (e, t, a, i, r) { var n = D.createEvent("Event"); if (!a) { a = {} } a.instance = k; n.initEvent(t, !i, !r); n.detail = a; e.dispatchEvent(n); return n }, Y = function (e, t) { var a; if (!i && (a = u.picturefill || H.pf)) { if (t && t.src && !e[$]("srcset")) { e.setAttribute("srcset", t.src) } a({ reevaluate: true, elements: [e] }) } else if (t && t.src) { e.src = t.src } }, Z = function (e, t) { return (getComputedStyle(e, null) || {})[t] }, s = function (e, t, a) { a = a || e.offsetWidth; while (a < H.minSize && t && !e._lazysizesWidth) { a = t.offsetWidth; t = t.parentNode } return a }, ee = function () { var a, i; var t = []; var r = []; var n = t; var s = function () { var e = n; n = t.length ? r : t; a = true; i = false; while (e.length) { e.shift()() } a = false }; var e = function (e, t) { if (a && !t) { e.apply(this, arguments) } else { n.push(e); if (!i) { i = true; (D.hidden ? I : U)(s) } } }; e._lsFlush = s; return e }(), te = function (a, e) { return e ? function () { ee(a) } : function () { var e = this; var t = arguments; ee(function () { a.apply(e, t) }) } }, ae = function (e) { var a; var i = 0; var r = H.throttleDelay; var n = H.ricTimeout; var t = function () { a = false; i = f.now(); e() }; var s = o && n > 49 ? function () { o(t, { timeout: n }); if (n !== H.ricTimeout) { n = H.ricTimeout } } : te(function () { I(t) }, true); return function (e) { var t; if (e = e === true) { n = 33 } if (a) { return } a = true; t = r - (f.now() - i); if (t < 0) { t = 0 } if (e || t < 9) { s() } else { I(s, t) } } }, ie = function (e) { var t, a; var i = 99; var r = function () { t = null; e() }; var n = function () { var e = f.now() - a; if (e < i) { I(n, i - e) } else { (o || r)(r) } }; return function () { a = f.now(); if (!t) { t = I(n, i) } } }, e = function () { var v, m, c, h, e; var y, z, g, p, C, b, A; var n = /^img$/i; var d = /^iframe$/i; var E = "onscroll" in u && !/(gle|ing)bot/.test(navigator.userAgent); var _ = 0; var w = 0; var M = 0; var N = -1; var L = function (e) { M--; if (!e || M < 0 || !e.target) { M = 0 } }; var x = function (e) { if (A == null) { A = Z(D.body, "visibility") == "hidden" } return A || !(Z(e.parentNode, "visibility") == "hidden" && Z(e, "visibility") == "hidden") }; var W = function (e, t) { var a; var i = e; var r = x(e); g -= t; b += t; p -= t; C += t; while (r && (i = i.offsetParent) && i != D.body && i != O) { r = (Z(i, "opacity") || 1) > 0; if (r && Z(i, "overflow") != "visible") { a = i.getBoundingClientRect(); r = C > a.left && p < a.right && b > a.top - 1 && g < a.bottom + 1 } } return r }; var t = function () { var e, t, a, i, r, n, s, o, l, u, f, c; var d = k.elements; if ((h = H.loadMode) && M < 8 && (e = d.length)) { t = 0; N++; for (; t < e; t++) { if (!d[t] || d[t]._lazyRace) { continue } if (!E || k.prematureUnveil && k.prematureUnveil(d[t])) { R(d[t]); continue } if (!(o = d[t][$]("data-expand")) || !(n = o * 1)) { n = w } if (!u) { u = !H.expand || H.expand < 1 ? O.clientHeight > 500 && O.clientWidth > 500 ? 500 : 370 : H.expand; k._defEx = u; f = u * H.expFactor; c = H.hFac; A = null; if (w < f && M < 1 && N > 2 && h > 2 && !D.hidden) { w = f; N = 0 } else if (h > 1 && N > 1 && M < 6) { w = u } else { w = _ } } if (l !== n) { y = innerWidth + n * c; z = innerHeight + n; s = n * -1; l = n } a = d[t].getBoundingClientRect(); if ((b = a.bottom) >= s && (g = a.top) <= z && (C = a.right) >= s * c && (p = a.left) <= y && (b || C || p || g) && (H.loadHidden || x(d[t])) && (m && M < 3 && !o && (h < 3 || N < 4) || W(d[t], n))) { R(d[t]); r = true; if (M > 9) { break } } else if (!r && m && !i && M < 4 && N < 4 && h > 2 && (v[0] || H.preloadAfterLoad) && (v[0] || !o && (b || C || p || g || d[t][$](H.sizesAttr) != "auto"))) { i = v[0] || d[t] } } if (i && !r) { R(i) } } }; var a = ae(t); var S = function (e) { var t = e.target; if (t._lazyCache) { delete t._lazyCache; return } L(e); K(t, H.loadedClass); Q(t, H.loadingClass); V(t, B); X(t, "lazyloaded") }; var i = te(S); var B = function (e) { i({ target: e.target }) }; var T = function (e, t) { var a = e.getAttribute("data-load-mode") || H.iframeLoadMode; if (a == 0) { e.contentWindow.location.replace(t) } else if (a == 1) { e.src = t } }; var F = function (e) { var t; var a = e[$](H.srcsetAttr); if (t = H.customMedia[e[$]("data-media") || e[$]("media")]) { e.setAttribute("media", t) } if (a) { e.setAttribute("srcset", a) } }; var s = te(function (t, e, a, i, r) { var n, s, o, l, u, f; if (!(u = X(t, "lazybeforeunveil", e)).defaultPrevented) { if (i) { if (a) { K(t, H.autosizesClass) } else { t.setAttribute("sizes", i) } } s = t[$](H.srcsetAttr); n = t[$](H.srcAttr); if (r) { o = t.parentNode; l = o && j.test(o.nodeName || "") } f = e.firesLoad || "src" in t && (s || n || l); u = { target: t }; K(t, H.loadingClass); if (f) { clearTimeout(c); c = I(L, 2500); V(t, B, true) } if (l) { G.call(o.getElementsByTagName("source"), F) } if (s) { t.setAttribute("srcset", s) } else if (n && !l) { if (d.test(t.nodeName)) { T(t, n) } else { t.src = n } } if (r && (s || l)) { Y(t, { src: n }) } } if (t._lazyRace) { delete t._lazyRace } Q(t, H.lazyClass); ee(function () { var e = t.complete && t.naturalWidth > 1; if (!f || e) { if (e) { K(t, H.fastLoadedClass) } S(u); t._lazyCache = true; I(function () { if ("_lazyCache" in t) { delete t._lazyCache } }, 9) } if (t.loading == "lazy") { M-- } }, true) }); var R = function (e) { if (e._lazyRace) { return } var t; var a = n.test(e.nodeName); var i = a && (e[$](H.sizesAttr) || e[$]("sizes")); var r = i == "auto"; if ((r || !m) && a && (e[$]("src") || e.srcset) && !e.complete && !J(e, H.errorClass) && J(e, H.lazyClass)) { return } t = X(e, "lazyunveilread").detail; if (r) { re.updateElem(e, true, e.offsetWidth) } e._lazyRace = true; M++; s(e, t, r, i, a) }; var r = ie(function () { H.loadMode = 3; a() }); var o = function () { if (H.loadMode == 3) { H.loadMode = 2 } r() }; var l = function () { if (m) { return } if (f.now() - e < 999) { I(l, 999); return } m = true; H.loadMode = 3; a(); q("scroll", o, true) }; return { _: function () { e = f.now(); k.elements = D.getElementsByClassName(H.lazyClass); v = D.getElementsByClassName(H.lazyClass + " " + H.preloadClass); q("scroll", a, true); q("resize", a, true); q("pageshow", function (e) { if (e.persisted) { var t = D.querySelectorAll("." + H.loadingClass); if (t.length && t.forEach) { U(function () { t.forEach(function (e) { if (e.complete) { R(e) } }) }) } } }); if (u.MutationObserver) { new MutationObserver(a).observe(O, { childList: true, subtree: true, attributes: true }) } else { O[P]("DOMNodeInserted", a, true); O[P]("DOMAttrModified", a, true); setInterval(a, 999) } q("hashchange", a, true);["focus", "mouseover", "click", "load", "transitionend", "animationend"].forEach(function (e) { D[P](e, a, true) }); if (/d$|^c/.test(D.readyState)) { l() } else { q("load", l); D[P]("DOMContentLoaded", a); I(l, 2e4) } if (k.elements.length) { t(); ee._lsFlush() } else { a() } }, checkElems: a, unveil: R, _aLSL: o } }(), re = function () { var a; var n = te(function (e, t, a, i) { var r, n, s; e._lazysizesWidth = i; i += "px"; e.setAttribute("sizes", i); if (j.test(t.nodeName || "")) { r = t.getElementsByTagName("source"); for (n = 0, s = r.length; n < s; n++) { r[n].setAttribute("sizes", i) } } if (!a.detail.dataAttr) { Y(e, a.detail) } }); var i = function (e, t, a) { var i; var r = e.parentNode; if (r) { a = s(e, r, a); i = X(e, "lazybeforesizes", { width: a, dataAttr: !!t }); if (!i.defaultPrevented) { a = i.detail.width; if (a && a !== e._lazysizesWidth) { n(e, r, i, a) } } } }; var e = function () { var e; var t = a.length; if (t) { e = 0; for (; e < t; e++) { i(a[e]) } } }; var t = ie(e); return { _: function () { a = D.getElementsByClassName(H.autosizesClass); q("resize", t) }, checkElems: t, updateElem: i } }(), t = function () { if (!t.i && D.getElementsByClassName) { t.i = true; re._(); e._() } }; return I(function () { H.init && t() }), k = { cfg: H, autoSizer: re, loader: e, init: t, uP: Y, aC: K, rC: Q, hC: J, fire: X, gW: s, rAF: ee } }(e, e.document, Date); e.lazySizes = t, "object" == typeof module && module.exports && (module.exports = t) }("undefined" != typeof window ? window : {});
