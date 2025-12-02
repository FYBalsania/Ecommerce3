document.addEventListener("DOMContentLoaded", function () {
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
  let mobileMenuTogglefn, toggleMobileMenu, handleDesktopMouseOver, handleDesktopMouseLeave,handleDesktopTouch,bwNavTitlefn,removeOverlay,hoverTimeout,hideOverlayTimeout; 
  let navHistory = [];
  const hoverIntentInstances = []; 
  const bwNavTitle = document.querySelector(".bw-navtitle span");  
  function animHeight() {
    const ulMenu = document.querySelector(".bw-menu ul.bw-navitems");
    const bwMenu = document.querySelector(".bw-menu");
    if (ulMenu && bwMenu) {
      if (ulMenu.classList.contains("bw-translate-0")) {
        bwMenu.style.marginTop = "0px";
      } else {
        bwMenu.style.marginTop = "40px";
      }
    }
  }
  function desktopRemoveEventListeners() {
    const bc = document.querySelectorAll(".b_c");
    const bwMenu = document.querySelector(".bw-menu");
    const overlay = document.querySelector(".menu-overlay");
    if (hoverTimeout) clearTimeout(hoverTimeout);
    if (hideOverlayTimeout) clearTimeout(hideOverlayTimeout);  
    hoverIntentInstances.forEach(instance => instance.remove());
    hoverIntentInstances.length = 0; 
    if (overlay) {
      overlay.removeEventListener("mouseenter", removeOverlay);
      overlay.removeEventListener("click", removeOverlay);
      overlay.removeAttribute("style");
      overlay.style.display = "none";
    }
    bc.forEach(item => {
      item.removeEventListener("mouseover", handleDesktopMouseOver);
      item.removeEventListener("touchstart", handleDesktopTouch);
      item.removeEventListener("mouseleave", handleDesktopMouseLeave);
      const menuAreas = item.querySelectorAll(".bw-navlevel2");      
      menuAreas.forEach(menuArea => {        
        menuArea.removeAttribute("style");
        menuArea.setAttribute("aria-hidden", "true");
      });      
      item.classList.remove("hovered");
    });
    bwMenu.removeAttribute("style");
    const navlevel2 = document.querySelectorAll(".bw-navlevel2");
    const navlevel3 = document.querySelectorAll(".bw-navlevel3");
    navlevel2.forEach((item) => item.setAttribute("aria-hidden", "true"));
    navlevel3.forEach((item) => item.setAttribute("aria-hidden", "true"));
    handleDesktopTouch = function () {};
  }
  function mobile(){            
      const menuItems = document.querySelectorAll(".bw-navlink.bw-navfold");
      const mobileMenuToggle = document.querySelector(".bw-navbtn");      
      const navlevel2 = document.querySelectorAll(".bw-navlevel2");
      const navlevel3 = document.querySelectorAll(".bw-navlevel3");
      navlevel2.forEach(item => item.setAttribute("aria-hidden", "true"))
      navlevel3.forEach(item =>  item.setAttribute("aria-hidden", "true"));
        toggleMobileMenu = function (event) {           
        const parentItem = event.currentTarget.closest(".bw-navitem");
        const parentUL = document.querySelector(".bw-navitems");     
        if (parentUL) {      
          parentItem.classList.add("bw-navitem--opened");
          parentItem.classList.add("bw-navitem--focused");
          parentUL.classList.add("bw-navitems--opened");
          if (parentUL.classList.contains("bw-translate-0")) {
            const closestDiv = parentItem.querySelector(".bw-navlevel2");
            if (closestDiv) {
              closestDiv.setAttribute("aria-hidden", "false");
              event.preventDefault();  
              navHistory.push(bwNavTitle.textContent);
              bwNavTitle.textContent = event.target.textContent;
              parentUL.classList.remove("bw-translate-0");
              parentUL.classList.add("bw-translate-1");
              bwNavTitle.parentElement.classList.add("bw-navtitle--back");
              const allbwNavLevel2Divs =
                document.querySelectorAll(".bw-navlevel2");
              allbwNavLevel2Divs.forEach((item) => {
                item.setAttribute("aria-hidden", "true");
              });    
            }      
          } else if (parentUL.classList.contains("bw-translate-1")) {
            event.preventDefault();        
            navHistory.push(bwNavTitle.textContent); 
            bwNavTitle.textContent = event.target.textContent;
            parentUL.classList.remove("bw-translate-1");
            parentUL.classList.add("bw-translate-2"); 
            bwNavTitle.parentElement.classList.add("bw-navtitle--back");             
            const closestDiv = parentItem.querySelector(".bw-navlevel3");
            closestDiv.setAttribute("aria-hidden", "false");      
          } else if (parentUL.classList.contains("bw-translate-2")) {
            event.preventDefault();
          }            
        }
        animHeight();
      }      
      bwNavTitlefn = function () {
        const parentUL = document.querySelector(".bw-navitems");    
        if (parentUL.classList.contains("bw-translate-2")) {      
              bwNavTitle.textContent = navHistory.pop(); 
              const li = document.querySelectorAll(".bw-navlevel2 li");
              const currentDiv = document.querySelectorAll(".bw-navlevel2 .bw-navlevel3");
              li.forEach(item => {
                item.classList.remove("bw-navitem--opened");
                item.classList.remove("bw-navitem--focused");
              });
              parentUL.classList.remove("bw-translate-2");
              parentUL.classList.add("bw-translate-1");
              currentDiv.forEach(item => item.setAttribute("aria-hidden", "true"));          
              const currentDivElem = parentUL.querySelector(".bw-navitem--focused .bw-navlevel2");
              currentDivElem.setAttribute("aria-hidden", "false");
        } else if (parentUL.classList.contains("bw-translate-1")) {             
              bwNavTitle.textContent = navHistory.pop(); 
              const li = document.querySelectorAll(".bw-translate-1 li");
              const currentDiv = document.querySelectorAll(".bw-navlevel2")
              li.forEach(item => {
                item.classList.remove("bw-navitem--opened");
                item.classList.remove("bw-navitem--focused");
              });
              parentUL.classList.remove("bw-translate-1");
              parentUL.classList.add("bw-translate-0");
              bwNavTitle.parentElement.classList.remove("bw-navtitle--back"); 
              currentDiv.forEach(item => item.setAttribute("aria-hidden", "true"));               
        }
        animHeight();
      }
      menuItems.forEach(item => {
        item.addEventListener("click", toggleMobileMenu);
      });
      bwNavTitle.addEventListener("click", bwNavTitlefn);      
      mobileMenuTogglefn = function (event) {
        const listing = document.getElementById("listing");
        if (listing) {
          const filterBtn = document.querySelector(".filter_button");
          filterBtn.classList.toggle("filter_button_hide");
        }
        event.preventDefault();
        const body = document.body;
        const ulMenu = body.querySelector(".bw-menu ul.bw-navitems");
        if (!body.classList.contains("bw-nav--opened")) {
          body.classList.add("bw-nav--opened");
          ulMenu.setAttribute("aria-hidden", "false");
          ulMenu.classList.add("bw-navlevel1--move", "bw-translate-0");
        } else {
          body.classList.remove("bw-nav--opened");
          const li = ulMenu.querySelectorAll("li");
          li.forEach((item) => {
            item.classList.remove("bw-navitem--opened");
            item.classList.remove("bw-navitem--focused");
          });
          ulMenu.setAttribute("aria-hidden", "true");
          ulMenu.classList.remove("bw-translate-1","bw-translate-2","bw-navlevel1--move","bw-translate-0");  
          bwNavTitle.parentElement.classList.remove("bw-navtitle--back");         
          bwNavTitle.textContent = "";
        }
        animHeight();
      }    
      mobileMenuToggle.addEventListener("click", mobileMenuTogglefn);
    } 

  function removeEventListeners(){
    const bwBtn = document.querySelector(".bw-btn");
    bwBtn.removeEventListener("click", mobileMenuTogglefn);
    const mainUL = document.querySelector(".bw-menu ul.bw-navitems");
    const allLis = mainUL.querySelectorAll("li");
    allLis.forEach(item => {
      item.classList.remove("bw-navitem--opened");
      item.classList.remove("bw-navitem--focused");      
    });
    mainUL.classList.remove("bw-translate-1","bw-translate-2","bw-navlevel1--move","bw-translate-0","bw-navitems--opened");    
    bwNavTitle.textContent = "";
    bwNavTitle.removeEventListener("click", bwNavTitlefn);
    bwNavTitle.parentElement.classList.remove("bw-navtitle--back"); 
    const body = document.body;
    body.classList.remove("bw-nav--opened");
    const menuItems = document.querySelectorAll(".bw-navlink.bw-navfold");
    menuItems.forEach(item => {
      item.removeEventListener("click", toggleMobileMenu);
    })
  }
  function desktop() {
    const bc = document.querySelectorAll(".b_c");
    const bwMenu = document.querySelector(".bw-menu");
    bwMenu.removeAttribute("style");
    const overlay = document.querySelector(".menu-overlay");    
    let delay = 20;     
      handleDesktopMouseOver = function (event) {        
        event.stopPropagation();
        clearTimeout(hoverTimeout);
        clearTimeout(hideOverlayTimeout);
        hoverTimeout = setTimeout(() => {
          bc.forEach(i => {            
            i.removeEventListener("touchstart", handleDesktopTouch);
            i.classList.remove("hovered");
            const menuArea = i.querySelector(".bw-navlevel2");
            if (menuArea) {
            menuArea.style.display = "none";
            menuArea.setAttribute("aria-hidden", "true");
            }
          });          
          this.classList.add("hovered");
          const directInsideLink = this.querySelector(".single_link");
          if (this.classList.contains("hovered")) {
            const menuArea = this.querySelector(".bw-navlevel2"); 
            if (menuArea) {
            menuArea.style.display = "block";
            menuArea.setAttribute("aria-hidden", "false");
            }
          }
          if (directInsideLink) {
          overlay.style.opacity = 0;
          overlay.style.display = "none";
          } else {
          overlay.style.opacity = 1;
          }
        }, delay);        
        overlay.style.display = "block";        
      };
      handleDesktopMouseLeave = function () {              
        clearTimeout(hoverTimeout);
        hideOverlayTimeout = setTimeout(() => {        
        if (!overlay.matches(':hover')) { 
        bc.forEach(i => {
           i.classList.remove("hovered")
            const menuArea = i.querySelector(".bw-navlevel2");
            if (menuArea) {
            menuArea.style.display = "none";
            menuArea.setAttribute("aria-hidden", "true");
            }
          }      
        )    
        overlay.style.display = "none"; 
        overlay.style.opacity = 0;       
        }
        }, 400);      
        
      };     

      handleDesktopTouch = function (event) {                         
          const menuArea = this.querySelector(".bw-navlevel2");
          if (!menuArea) {            
            const anchor = this.querySelector("a");
            if (overlay) {
              overlay.style.display = "none";
            }
            anchor.click();
            return;
          } 
          if (event.target.classList.contains("bw-navfold")) {
            event.preventDefault();
          } else {
            return;
          }   
          if (this.classList.contains("hovered")) {                     
            this.classList.remove("hovered");
            if (menuArea) {
            menuArea.style.display = "none";
            menuArea.setAttribute("aria-hidden", "true");
            }          
            if (overlay) {
              overlay.style.display = "none";
            }
          } else {            
            bc.forEach(i => {
              if (i !== this && i.classList.contains("hovered")) {
                i.classList.remove("hovered");
                const otherMenuArea = i.querySelector(".bw-navlevel2");
                if (otherMenuArea) {
                  otherMenuArea.style.display = "none";
                  otherMenuArea.setAttribute("aria-hidden", "true");
                }
              }
            });            
            this.classList.add("hovered");
            if (menuArea) {
            menuArea.style.display = "block";
            menuArea.setAttribute("aria-hidden", "false");
            }            
            if (overlay) {
              overlay.style.cssText = "opacity: 1; display: block;";
            }
          }
      };   

    if (Modernizr.mq("(min-width: 1024px)")) {
      let dataWhatInput = document.documentElement.getAttribute("data-whatinput");
      if (dataWhatInput === "touch") {
        bc.forEach((item) => {
          if (!item.hasTouchListener) {
            item.addEventListener("touchstart", (event) => handleDesktopTouch.call(item, event));
            item.hasTouchListener = true;
          }
        });
      }
    }

    bc.forEach(item => {
      const hoverIntentInstance = hoverintent(item,handleDesktopMouseOver,handleDesktopMouseLeave).options({
        sensitivity: 4, 
        interval: 80, 
        timeout: 150, 
      });
      hoverIntentInstances.push(hoverIntentInstance);      
    });
    removeOverlay = function () {
      clearTimeout(hideOverlayTimeout);
      clearTimeout(hoverTimeout);
      bc.forEach(i => {
        i.classList.remove("hovered");
        const menuArea = i.querySelector(".bw-navlevel2");
        if (menuArea) {
        menuArea.style.display = "none";
        menuArea.setAttribute("aria-hidden", "true");
        }
      });
      overlay.style.display = "none";
      overlay.style.opacity = 0;
    };    
    overlay.addEventListener("mouseenter", removeOverlay);
    overlay.addEventListener("click", removeOverlay);
  }
   
  if (Modernizr.mq("(min-width: 1024px)")) {
      desktop()
  } else {      
      mobile();
  }

  let currentMode = '';     
    window.addEventListener("resize", debounce(function () {
      if (Modernizr.mq("(min-width: 1024px)")) {            
        if (currentMode !== 'desktop') {
          removeEventListeners();          
          desktopRemoveEventListeners();
          desktop(); 
          currentMode = 'desktop';           
        }
      } else if (Modernizr.mq("(max-width: 1023px)")) {        
        if (currentMode !== 'mobile') { 
          desktopRemoveEventListeners();
          removeEventListeners();         
          mobile(); 
          currentMode = 'mobile';           
        }
      }
    }, 200));

});
// hover intent
!function(e,t){if("function"==typeof define&&define.amd)define("hoverintent",["module"],t);else if("undefined"!=typeof exports)t(module);else{var n={exports:{}};t(n),e.hoverintent=n.exports}}(this,function(e){"use strict";var t=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var o in n)Object.prototype.hasOwnProperty.call(n,o)&&(e[o]=n[o])}return e};e.exports=function(e,n,o){function i(e,t){return y&&(y=clearTimeout(y)),b=0,p?void 0:o.call(e,t)}function r(e){m=e.clientX,d=e.clientY}function u(e,t){if(y&&(y=clearTimeout(y)),Math.abs(h-m)+Math.abs(E-d)<x.sensitivity)return b=1,p?void 0:n.call(e,t);h=m,E=d,y=setTimeout(function(){u(e,t)},x.interval)}function s(t){return L=!0,y&&(y=clearTimeout(y)),e.removeEventListener("mousemove",r,!1),1!==b&&(h=t.clientX,E=t.clientY,e.addEventListener("mousemove",r,!1),y=setTimeout(function(){u(e,t)},x.interval)),this}function c(t){return L=!1,y&&(y=clearTimeout(y)),e.removeEventListener("mousemove",r,!1),1===b&&(y=setTimeout(function(){i(e,t)},x.timeout)),this}function v(t){L||(p=!0,n.call(e,t))}function a(t){!L&&p&&(p=!1,o.call(e,t))}function f(){e.addEventListener("focus",v,!1),e.addEventListener("blur",a,!1)}function l(){e.removeEventListener("focus",v,!1),e.removeEventListener("blur",a,!1)}var m,d,h,E,L=!1,p=!1,T={},b=0,y=0,x={sensitivity:7,interval:100,timeout:0,handleFocus:!1};return T.options=function(e){var n=e.handleFocus!==x.handleFocus;return x=t({},x,e),n&&(x.handleFocus?f():l()),T},T.remove=function(){e&&(e.removeEventListener("mouseover",s,!1),e.removeEventListener("mouseout",c,!1),l())},e&&(e.addEventListener("mouseover",s,!1),e.addEventListener("mouseout",c,!1)),T}});

