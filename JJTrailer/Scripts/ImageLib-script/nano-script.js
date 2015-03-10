
$(document).ready(function () {
    $.post('/Admin/ImageLibs/getimages', null)
  .done(function( items ) {
  
      console.log(items);
      $("#nanoGallery4").nanoGallery({
          thumbnailWidth: 100,
          thumbnailHeight: 100,
          
          items: items
      });
 });

  });
        