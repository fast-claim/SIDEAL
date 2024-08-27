document.addEventListener('DOMContentLoaded', function() {
    let currentIndex = 0;
    const images = document.querySelectorAll('.image');
    const totalImages = images.length;

    function showImage(index) {
      images.forEach((image, i) => {
        image.classList.add('opacity-0');
        image.classList.remove('opacity-100');
      });
      images[index].classList.remove('opacity-0');
      images[index].classList.add('opacity-100');
    }

    document.getElementById('next').addEventListener('click', () => {
      currentIndex = (currentIndex + 1) % totalImages;
      showImage(currentIndex);
    });

    document.getElementById('prev').addEventListener('click', () => {
      currentIndex = (currentIndex - 1 + totalImages) % totalImages;
      showImage(currentIndex);
    });

    // Show the first image initially
    showImage(currentIndex);
  });