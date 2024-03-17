
//(function ($) {
//    $(document).ready(function () {

//        // Scroll to Top
//        jQuery('.scrollto-top').click(function () {
//            jQuery('html').animate({ 'scrollTop': '0px' }, 400);
//            return false;
//        });

//        jQuery(window).scroll(function () {
//            var upto = jQuery(window).scrollTop();
//            if (upto > 500) {
//                jQuery('.scrollto-top').fadeIn();
//            } else {
//                jQuery('.scrollto-top').fadeOut();
//            }
//        });


//        $(".owl-csel1").owlCarousel({
//            items: 5.5,
//            autoplay: true,
//            autoplayTimeout: 3500,
//            startPosition: 0,
//            center: true,
//            rtl: false,
//            loop: true,
//            margin: 15,
//            dots: false,
//            nav: false,
//            responsive: {
//                0: {
//                    items: 2,
//                },
//                768: {
//                    items: 2,
//                },
//                992: {
//                    items: 5.2,
//                },
//                1200: {
//                    items: 5.5,
//                }
//            }

//        })
//    });
//})(jQuery);

//// Get all anchor tags with the specified data attributes
//const anchorTags = document.querySelectorAll('a[data-bId][data-vId]');
//// Add a click event listener to each anchor tag
//anchorTags.forEach(tag => {
//    tag.addEventListener('click', function (event) {
//        event.preventDefault(); // Prevent the default link behavior (e.g., navigating to a new page)
//        // Get the data attributes from the clicked anchor tag
//        const brandId = tag.getAttribute('data-bId');
//        const visitId = tag.getAttribute('data-vId');
//        const webSite = tag.getAttribute('data-wSite');
//        // Construct the URL with route parameters using the data attributes
//        const apiUrl = `https://dashboard.cybergagency.com/clickint/?website=${webSite}&brandId=${brandId}&visitId=${visitId}`;
//        // Make a GET request to the API with the constructed URL
//        fetch(apiUrl)
//            .then(response => {
//                if (response.ok) {
//                    // Handle a successful response
//                    console.log('API request succeeded');
//                } else {
//                    // Handle errors
//                    console.error('API request failed');
//                }
//            })
//            .catch(error => {
//                console.error('API request error:', error);
//            });
//        var link = atob(tag.getAttribute('href'));
//        window.open(link, '_blank');
//    });
//});

//function getMimeType(url) {
//    let extension = url.split('.').pop().toLowerCase();
//    switch (extension) {
//        case 'jpg':
//        case 'jpeg':
//            return 'image/jpeg';
//        case 'png':
//            return 'image/png';
//        case 'svg':
//            return 'image/svg+xml';
//        // Add cases for other file types as needed
//        default:
//            return 'application/octet-stream'; // Default type or throw error
//    }
//}



//async function convertImageToBase64(url) {
//    try {
//        const response = await fetch(url);
//        const blob = await response.blob();

//        return new Promise((resolve, reject) => {
//            const reader = new FileReader();
//            reader.onloadend = function () {
//                const base64data = reader.result;
//                resolve(base64data);
//            };
//            reader.onerror = reject;
//            reader.readAsDataURL(blob);
//        });
//    } catch (error) {
//        console.error('Error:', error);
//        return null;
//    }
//}





////window.onload = function () {
////    var images = document.querySelectorAll('.dynamic-image');
////    images.forEach(img => {
////        var imageUrl = img.getAttribute('data-image-url');
////        convertImageToBase64(imageUrl).then(base64String => {
////            console.log(base64String);
////            img.src = base64String;
////            //img.removeAttribute('data-image-url'); // Remove the attribute after setting src
////        });
////    });
////};