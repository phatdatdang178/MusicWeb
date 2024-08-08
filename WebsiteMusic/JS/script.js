document.querySelectorAll('.sidebar a.custom-link').forEach(item => {
    item.addEventListener('click', () => {
        document.querySelector('.list-group .list-group-item-action.active').classList.remove('active');
        document.querySelector('.list-group .list-group-item-action[href="#thuvien"]').classList.add('active');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    // Lấy tất cả các nút trong khung chứa
    const buttons = document.querySelectorAll('.button-container button');
    
    // Hàm để xử lý việc chọn nút
    const handleButtonClick = (event) => {
        // Xóa lớp 'active' khỏi tất cả các nút
        buttons.forEach(btn => btn.classList.remove('active'));
        // Thêm lớp 'active' cho nút hiện tại
        event.target.classList.add('active');
    };
    
    // Gán sự kiện click cho từng nút
    buttons.forEach(button => {
        button.addEventListener('click', handleButtonClick);
    });
});
//chuyển slide
document.addEventListener('DOMContentLoaded', function() {
    const carouselInner = document.querySelector('.carousel-inner');
    const carouselItems = Array.from(carouselInner.children);
    let currentIndex = 0;

    function showSlide(index) {
        carouselItems.forEach((item, i) => {
            item.style.display = (i === index) ? 'flex' : 'none'; // Hiển thị slide hiện tại
        });
    }

    function nextSlide() {
        currentIndex = (currentIndex + 1) % carouselItems.length;
        showSlide(currentIndex);
    }

    function prevSlide() {
        currentIndex = (currentIndex - 1 + carouselItems.length) % carouselItems.length;
        showSlide(currentIndex);
    }

    // Hiển thị slide đầu tiên
    showSlide(currentIndex);

    // Xử lý sự kiện click cho nút 'Next'
    document.querySelector('.carousel-control-next').addEventListener('click', function() {
        nextSlide();
    });

    // Xử lý sự kiện click cho nút 'Prev'
    document.querySelector('.carousel-control-prev').addEventListener('click', function() {
        prevSlide();
    });
});

document.addEventListener('DOMContentLoaded', function() {
    // Tải nội dung của trang "Khám Phá" khi trang được tải lần đầu tiên
    loadContent('khampha.html');

    // Xử lý sự kiện khi nhấp vào liên kết trong sidebar
    document.querySelectorAll('.list-group-item').forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault(); // Ngăn chặn hành vi mặc định của liên kết
            var target = this.getAttribute('href'); // Lấy đường dẫn đến nội dung cần tải
            loadContent(target); // Gọi hàm tải nội dung
        });
    });

    function loadContent(url) {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                document.querySelector('.content').innerHTML = data;
                // Khởi tạo lại các thành phần Bootstrap như carousel nếu có
                var carousels = document.querySelectorAll('.carousel');
                carousels.forEach(carousel => new bootstrap.Carousel(carousel));
            })
            .catch(error => {
                console.error('Có lỗi xảy ra:', error);
                document.querySelector('.content').innerHTML = 'Không thể tải nội dung.';
            });
    }
});

document.addEventListener('DOMContentLoaded', function() {
    loadContent('khampha.html');

    document.querySelectorAll('.list-group-item').forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            var target = this.getAttribute('href');
            loadContent(target);
        });
    });

    function loadContent(url) {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                document.querySelector('.content').innerHTML = data;
                // Khởi tạo lại các thành phần Bootstrap như carousel nếu có
                var carousels = document.querySelectorAll('.carousel');
                carousels.forEach(carousel => new bootstrap.Carousel(carousel));

                // Gán sự kiện click cho các nút mới
                attachButtonEventListeners();
            })
            .catch(error => {
                console.error('Có lỗi xảy ra:', error);
                document.querySelector('.content').innerHTML = 'Không thể tải nội dung.';
            });
    }

    function attachButtonEventListeners() {
        // Lấy tất cả các nút trong phần nội dung mới tải
        const buttons = document.querySelectorAll('.button-container button');
        
        // Xử lý sự kiện click cho từng nút
        const handleButtonClick = (event) => {
            buttons.forEach(btn => btn.classList.remove('active'));
            event.target.classList.add('active');
        };

        buttons.forEach(button => {
            button.addEventListener('click', handleButtonClick);
        });
    }
});



