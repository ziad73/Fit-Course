// Global variables
let currentUser = null;
let courses = [];
let enrolledCourses = [];
let wishlist = [];

// Initialize the application
document.addEventListener('DOMContentLoaded', function() {
    initializeApp();
});

function initializeApp() {
    loadDataFromStorage();
    setupEventListeners();
    renderPage();
    updateUserMenu();
}

// Local Storage Management
function saveDataToStorage() {
    localStorage.setItem('learnhub_user', JSON.stringify(currentUser));
    localStorage.setItem('learnhub_enrolled', JSON.stringify(enrolledCourses));
    localStorage.setItem('learnhub_wishlist', JSON.stringify(wishlist));
}

function loadDataFromStorage() {
    currentUser = JSON.parse(localStorage.getItem('learnhub_user')) || null;
    enrolledCourses = JSON.parse(localStorage.getItem('learnhub_enrolled')) || [];
    wishlist = JSON.parse(localStorage.getItem('learnhub_wishlist')) || [];
    
    // Load sample courses if none exist
    if (localStorage.getItem('learnhub_courses') === null) {
        loadSampleCourses();
    } else {
        courses = JSON.parse(localStorage.getItem('learnhub_courses'));
    }
}

function loadSampleCourses() {
    courses = [
        {
            id: 1,
            title: "Complete Web Development Bootcamp",
            instructor: "Dr. Angela Yu",
            price: 89.99,
            originalPrice: 199.99,
            rating: 4.8,
            students: 125000,
            image: "https://images.unsplash.com/photo-1461749280684-dccba630e2f6?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "programming",
            description: "Learn web development from scratch with this comprehensive bootcamp covering HTML, CSS, JavaScript, Node.js, and more.",
            duration: "44 hours",
            lessons: 490,
            level: "Beginner",
            language: "English",
            lastUpdated: "2024-01-15"
        },
        {
            id: 2,
            title: "UI/UX Design Masterclass",
            instructor: "Sarah Johnson",
            price: 69.99,
            originalPrice: 149.99,
            rating: 4.7,
            students: 45000,
            image: "https://images.unsplash.com/photo-1561070791-2526d30994b5?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "design",
            description: "Master the principles of UI/UX design with practical projects and real-world applications.",
            duration: "32 hours",
            lessons: 280,
            level: "Intermediate",
            language: "English",
            lastUpdated: "2024-01-10"
        },
        {
            id: 3,
            title: "Digital Marketing Strategy",
            instructor: "Mike Chen",
            price: 79.99,
            originalPrice: 179.99,
            rating: 4.6,
            students: 67000,
            image: "https://images.unsplash.com/photo-1460925895917-afdab827c52f?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "marketing",
            description: "Learn comprehensive digital marketing strategies including SEO, social media, and content marketing.",
            duration: "28 hours",
            lessons: 220,
            level: "Beginner",
            language: "English",
            lastUpdated: "2024-01-05"
        },
        {
            id: 4,
            title: "Python for Data Science",
            instructor: "Dr. Emily Rodriguez",
            price: 94.99,
            originalPrice: 199.99,
            rating: 4.9,
            students: 89000,
            image: "https://images.unsplash.com/photo-1526379095098-d400fd0bf935?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "programming",
            description: "Master Python programming for data analysis, machine learning, and scientific computing.",
            duration: "52 hours",
            lessons: 380,
            level: "Intermediate",
            language: "English",
            lastUpdated: "2024-01-20"
        },
        {
            id: 5,
            title: "Business Strategy & Leadership",
            instructor: "Prof. David Thompson",
            price: 119.99,
            originalPrice: 249.99,
            rating: 4.7,
            students: 34000,
            image: "https://images.unsplash.com/photo-1552664730-d307ca884978?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "business",
            description: "Develop strategic thinking and leadership skills for modern business challenges.",
            duration: "36 hours",
            lessons: 310,
            level: "Advanced",
            language: "English",
            lastUpdated: "2024-01-12"
        },
        {
            id: 6,
            title: "Mobile App Development with React Native",
            instructor: "Alex Kumar",
            price: 84.99,
            originalPrice: 169.99,
            rating: 4.8,
            students: 56000,
            image: "https://images.unsplash.com/photo-1512941937669-90a1b58e7e9c?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80",
            category: "programming",
            description: "Build cross-platform mobile apps using React Native and JavaScript.",
            duration: "40 hours",
            lessons: 320,
            level: "Intermediate",
            language: "English",
            lastUpdated: "2024-01-18"
        }
    ];
    localStorage.setItem('learnhub_courses', JSON.stringify(courses));
}

// Event Listeners Setup
function setupEventListeners() {
    // Dark mode toggle
    const darkModeToggle = document.getElementById('darkModeToggle');
    if (darkModeToggle) {
        darkModeToggle.addEventListener('click', toggleDarkMode);
    }

    // Search functionality
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', handleSearch);
    }

    // Login form
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', handleLogin);
    }

    // Signup form
    const signupForm = document.getElementById('signupForm');
    if (signupForm) {
        signupForm.addEventListener('submit', handleSignup);
    }

    // Category filters
    const categoryLinks = document.querySelectorAll('[data-category]');
    categoryLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            filterByCategory(link.dataset.category);
        });
    });
}

// Dark Mode Toggle
function toggleDarkMode() {
    const body = document.body;
    const icon = document.querySelector('#darkModeToggle i');
    
    if (body.getAttribute('data-theme') === 'dark') {
        body.removeAttribute('data-theme');
        icon.className = 'fas fa-moon';
        localStorage.setItem('learnhub_theme', 'light');
    } else {
        body.setAttribute('data-theme', 'dark');
        icon.className = 'fas fa-sun';
        localStorage.setItem('learnhub_theme', 'dark');
    }
}

// Load saved theme
function loadSavedTheme() {
    const savedTheme = localStorage.getItem('learnhub_theme');
    const icon = document.querySelector('#darkModeToggle i');
    
    if (savedTheme === 'dark') {
        document.body.setAttribute('data-theme', 'dark');
        if (icon) icon.className = 'fas fa-sun';
    }
}

// Search Functionality
function handleSearch(e) {
    e.preventDefault();
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    
    if (searchTerm.trim() === '') {
        renderCourses(courses);
        return;
    }
    
    const filteredCourses = courses.filter(course => 
        course.title.toLowerCase().includes(searchTerm) ||
        course.instructor.toLowerCase().includes(searchTerm) ||
        course.category.toLowerCase().includes(searchTerm)
    );
    
    renderCourses(filteredCourses);
}

// Category Filtering
function filterByCategory(category) {
    const filteredCourses = courses.filter(course => course.category === category);
    renderCourses(filteredCourses);
}

// Authentication Functions
function handleLogin(e) {
    e.preventDefault();
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    
    // Mock login - in real app, this would validate against backend
    if (email && password) {
        currentUser = {
            id: 1,
            name: email.split('@')[0],
            email: email,
            avatar: `https://ui-avatars.com/api/?name=${email.split('@')[0]}&background=random`
        };
        
        saveDataToStorage();
        updateUserMenu();
        
        // Show success message
        showNotification('Login successful!', 'success');
        
        // Redirect to dashboard
        window.location.href = 'dashboard.html';
    }
}

function handleSignup(e) {
    e.preventDefault();
    const name = document.getElementById('signupName').value;
    const email = document.getElementById('signupEmail').value;
    const password = document.getElementById('signupPassword').value;
    
    if (name && email && password) {
        currentUser = {
            id: Date.now(),
            name: name,
            email: email,
            avatar: `https://ui-avatars.com/api/?name=${name}&background=random`
        };
        
        saveDataToStorage();
        updateUserMenu();
        
        // Show success message
        showNotification('Account created successfully!', 'success');
        
        // Redirect to dashboard
        window.location.href = 'dashboard.html';
    }
}

function logout() {
    currentUser = null;
    saveDataToStorage();
    updateUserMenu();
    showNotification('Logged out successfully!', 'info');
}

// User Menu Management
function updateUserMenu() {
    const userMenu = document.getElementById('userMenu');
    if (!userMenu) return;
    
    if (currentUser) {
        userMenu.innerHTML = `
            <div class="dropdown">
                <button class="btn btn-outline-primary dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    <img src="${currentUser.avatar}" alt="Avatar" class="rounded-circle me-2" width="24" height="24">
                    ${currentUser.name}
                </button>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="dashboard.html">
                        <i class="fas fa-tachometer-alt me-2"></i>Dashboard
                    </a></li>
                    <li><a class="dropdown-item" href="profile.html">
                        <i class="fas fa-user me-2"></i>Profile
                    </a></li>
                    <li><a class="dropdown-item" href="#" onclick="showWishlist()">
                        <i class="fas fa-heart me-2"></i>Wishlist
                    </a></li>
                    <li><hr class="dropdown-divider"></li>
                    <li><a class="dropdown-item" href="#" onclick="logout()">
                        <i class="fas fa-sign-out-alt me-2"></i>Logout
                    </a></li>
                </ul>
            </div>
        `;
    } else {
        userMenu.innerHTML = `
            <div class="dropdown">
                <button class="btn btn-primary dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    <i class="fas fa-user me-2"></i>Sign In
                </button>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="login.html">Sign In</a></li>
                    <li><a class="dropdown-item" href="signup.html">Sign Up</a></li>
                </ul>
            </div>
        `;
    }
}

// Course Management
function enrollInCourse(courseId) {
    if (!currentUser) {
        showNotification('Please login to enroll in courses', 'warning');
        return;
    }
    
    const course = courses.find(c => c.id === courseId);
    if (!course) return;
    
    const existingEnrollment = enrolledCourses.find(ec => ec.courseId === courseId);
    if (existingEnrollment) {
        showNotification('You are already enrolled in this course', 'info');
        return;
    }
    
    const enrollment = {
        courseId: courseId,
        enrolledAt: new Date().toISOString(),
        progress: 0,
        completedLessons: [],
        lastAccessed: new Date().toISOString()
    };
    
    enrolledCourses.push(enrollment);
    saveDataToStorage();
    showNotification(`Successfully enrolled in ${course.title}!`, 'success');
}

function addToWishlist(courseId) {
    if (!currentUser) {
        showNotification('Please login to add courses to wishlist', 'warning');
        return;
    }
    
    if (wishlist.includes(courseId)) {
        wishlist = wishlist.filter(id => id !== courseId);
        showNotification('Course removed from wishlist', 'info');
    } else {
        wishlist.push(courseId);
        showNotification('Course added to wishlist', 'success');
    }
    
    saveDataToStorage();
}

function markLessonComplete(courseId, lessonId) {
    const enrollment = enrolledCourses.find(ec => ec.courseId === courseId);
    if (!enrollment) return;
    
    if (!enrollment.completedLessons.includes(lessonId)) {
        enrollment.completedLessons.push(lessonId);
        enrollment.progress = Math.round((enrollment.completedLessons.length / 10) * 100); // Assuming 10 lessons per course
        enrollment.lastAccessed = new Date().toISOString();
        saveDataToStorage();
    }
}

// Rendering Functions
function renderPage() {
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';
    
    switch (currentPage) {
        case 'index.html':
        case '':
            renderFeaturedCourses();
            break;
        case 'courses.html':
            renderCourses(courses);
            break;
        case 'course-detail.html':
            renderCourseDetail();
            break;
        case 'dashboard.html':
            renderDashboard();
            break;
    }
}

function renderFeaturedCourses() {
    const featuredCoursesContainer = document.getElementById('featuredCourses');
    if (!featuredCoursesContainer) return;
    
    const featuredCourses = courses.slice(0, 3);
    featuredCoursesContainer.innerHTML = featuredCourses.map(course => createCourseCard(course)).join('');
}

function renderCourses(coursesToRender) {
    const coursesContainer = document.getElementById('coursesContainer');
    if (!coursesContainer) return;
    
    coursesContainer.innerHTML = coursesToRender.map(course => createCourseCard(course)).join('');
}

function createCourseCard(course) {
    const isEnrolled = enrolledCourses.some(ec => ec.courseId === course.id);
    const isInWishlist = wishlist.includes(course.id);
    
    return `
        <div class="col-lg-4 col-md-6 mb-4">
            <div class="card course-card h-100">
                <img src="${course.image}" class="card-img-top" alt="${course.title}" style="height: 200px; object-fit: cover;">
                <div class="card-body d-flex flex-column">
                    <div class="d-flex justify-content-between align-items-start mb-2">
                        <span class="badge bg-primary">${course.category}</span>
                        <button class="btn btn-sm btn-outline-danger" onclick="addToWishlist(${course.id})">
                            <i class="fas fa-heart ${isInWishlist ? 'text-danger' : ''}"></i>
                        </button>
                    </div>
                    <h5 class="card-title">${course.title}</h5>
                    <p class="card-text text-muted">${course.description.substring(0, 100)}...</p>
                    <div class="mt-auto">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <span class="course-instructor">by ${course.instructor}</span>
                            <div class="course-rating">
                                <i class="fas fa-star"></i>
                                <span>${course.rating}</span>
                                <span class="text-muted">(${course.students.toLocaleString()})</span>
                            </div>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <span class="course-price">$${course.price}</span>
                                <small class="text-muted text-decoration-line-through ms-2">$${course.originalPrice}</small>
                            </div>
                            ${isEnrolled ? 
                                '<span class="badge bg-success">Enrolled</span>' : 
                                '<button class="btn btn-primary btn-sm" onclick="enrollInCourse(' + course.id + ')">Enroll Now</button>'
                            }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;
}

function renderCourseDetail() {
    const urlParams = new URLSearchParams(window.location.search);
    const courseId = parseInt(urlParams.get('id'));
    
    if (!courseId) {
        window.location.href = 'courses.html';
        return;
    }
    
    const course = courses.find(c => c.id === courseId);
    if (!course) {
        window.location.href = 'courses.html';
        return;
    }
    
    // Render course detail page content
    const courseHero = document.querySelector('.course-hero');
    if (courseHero) {
        courseHero.innerHTML = `
            <div class="container">
                <div class="row">
                    <div class="col-lg-8">
                        <h1 class="display-5 fw-bold mb-3">${course.title}</h1>
                        <p class="lead mb-3">${course.description}</p>
                        <div class="d-flex flex-wrap gap-3 mb-3">
                            <span class="badge bg-primary">${course.category}</span>
                            <span class="badge bg-secondary">${course.level}</span>
                            <span class="badge bg-info">${course.language}</span>
                        </div>
                        <div class="d-flex flex-wrap gap-4 text-muted">
                            <span><i class="fas fa-clock me-2"></i>${course.duration}</span>
                            <span><i class="fas fa-play-circle me-2"></i>${course.lessons} lessons</span>
                            <span><i class="fas fa-users me-2"></i>${course.students.toLocaleString()} students</span>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-body text-center">
                                <h3 class="course-price mb-3">$${course.price}</h3>
                                <small class="text-muted text-decoration-line-through">$${course.originalPrice}</small>
                                <div class="mt-3">
                                    ${enrolledCourses.some(ec => ec.courseId === course.id) ? 
                                        '<button class="btn btn-success w-100" disabled>Already Enrolled</button>' : 
                                        '<button class="btn btn-primary w-100" onclick="enrollInCourse(' + course.id + ')">Enroll Now</button>'
                                    }
                                </div>
                                <button class="btn btn-outline-primary w-100 mt-2" onclick="addToWishlist(${course.id})">
                                    <i class="fas fa-heart me-2"></i>Add to Wishlist
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
    
    // Render syllabus
    renderSyllabus(course);
}

function renderSyllabus(course) {
    const syllabusContainer = document.getElementById('courseSyllabus');
    if (!syllabusContainer) return;
    
    const lessons = generateSampleLessons(course);
    const isEnrolled = enrolledCourses.some(ec => ec.courseId === course.id);
    
    syllabusContainer.innerHTML = `
        <h3 class="mb-4">Course Content</h3>
        <div class="course-syllabus">
            ${lessons.map((lesson, index) => `
                <div class="lesson-item ${isEnrolled && lesson.completed ? 'completed' : ''}" 
                     onclick="${isEnrolled ? 'markLessonComplete(' + course.id + ', ' + index + ')' : ''}">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <i class="fas fa-play-circle me-2"></i>
                            <span>${lesson.title}</span>
                        </div>
                        <div>
                            <span class="text-muted me-3">${lesson.duration}</span>
                            ${isEnrolled && lesson.completed ? 
                                '<i class="fas fa-check-circle text-success"></i>' : 
                                '<i class="fas fa-lock text-muted"></i>'
                            }
                        </div>
                    </div>
                </div>
            `).join('')}
        </div>
    `;
}

function generateSampleLessons(course) {
    const lessonTitles = [
        "Introduction to the Course",
        "Getting Started with the Basics",
        "Core Concepts and Fundamentals",
        "Practical Examples and Exercises",
        "Advanced Techniques and Best Practices",
        "Real-world Applications",
        "Troubleshooting and Common Issues",
        "Optimization and Performance",
        "Final Project and Implementation",
        "Course Wrap-up and Next Steps"
    ];
    
    return lessonTitles.map((title, index) => ({
        title: title,
        duration: Math.floor(Math.random() * 30) + 15 + " min",
        completed: false
    }));
}

function renderDashboard() {
    if (!currentUser) {
        window.location.href = 'index.html';
        return;
    }
    
    renderDashboardStats();
    renderEnrolledCourses();
    renderWishlist();
}

function renderDashboardStats() {
    const statsContainer = document.getElementById('dashboardStats');
    if (!statsContainer) return;
    
    const totalCourses = enrolledCourses.length;
    const totalProgress = enrolledCourses.reduce((sum, ec) => sum + ec.progress, 0);
    const averageProgress = totalCourses > 0 ? Math.round(totalProgress / totalCourses) : 0;
    const totalWishlist = wishlist.length;
    
    statsContainer.innerHTML = `
        <div class="row text-center">
            <div class="col-md-3">
                <h3 class="display-6 fw-bold">${totalCourses}</h3>
                <p>Enrolled Courses</p>
            </div>
            <div class="col-md-3">
                <h3 class="display-6 fw-bold">${averageProgress}%</h3>
                <p>Average Progress</p>
            </div>
            <div class="col-md-3">
                <h3 class="display-6 fw-bold">${totalWishlist}</h3>
                <p>Wishlist Items</p>
            </div>
            <div class="col-md-3">
                <h3 class="display-6 fw-bold">${new Date().getFullYear()}</h3>
                <p>Learning Year</p>
            </div>
        </div>
    `;
}

function renderEnrolledCourses() {
    const enrolledContainer = document.getElementById('enrolledCourses');
    if (!enrolledContainer) return;
    
    if (enrolledCourses.length === 0) {
        enrolledContainer.innerHTML = `
            <div class="text-center py-5">
                <i class="fas fa-graduation-cap fa-3x text-muted mb-3"></i>
                <h4>No courses enrolled yet</h4>
                <p class="text-muted">Start your learning journey by enrolling in a course!</p>
                <a href="courses.html" class="btn btn-primary">Browse Courses</a>
            </div>
        `;
        return;
    }
    
    enrolledContainer.innerHTML = enrolledCourses.map(enrollment => {
        const course = courses.find(c => c.id === enrollment.courseId);
        if (!course) return '';
        
        return `
            <div class="col-lg-6 mb-4">
                <div class="dashboard-card">
                    <div class="row">
                        <div class="col-md-4">
                            <img src="${course.image}" alt="${course.title}" class="img-fluid rounded">
                        </div>
                        <div class="col-md-8">
                            <h5>${course.title}</h5>
                            <p class="text-muted mb-2">by ${course.instructor}</p>
                            <div class="mb-3">
                                <div class="d-flex justify-content-between mb-1">
                                    <small>Progress</small>
                                    <small>${enrollment.progress}%</small>
                                </div>
                                <div class="progress">
                                    <div class="progress-bar" style="width: ${enrollment.progress}%"></div>
                                </div>
                            </div>
                            <a href="course-detail.html?id=${course.id}" class="btn btn-primary btn-sm">Continue Learning</a>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }).join('');
}

function renderWishlist() {
    const wishlistContainer = document.getElementById('wishlistContainer');
    if (!wishlistContainer) return;
    
    if (wishlist.length === 0) {
        wishlistContainer.innerHTML = `
            <div class="text-center py-5">
                <i class="fas fa-heart fa-3x text-muted mb-3"></i>
                <h4>Your wishlist is empty</h4>
                <p class="text-muted">Add courses to your wishlist to save them for later!</p>
                <a href="courses.html" class="btn btn-primary">Browse Courses</a>
            </div>
        `;
        return;
    }
    
    const wishlistCourses = courses.filter(course => wishlist.includes(course.id));
    wishlistContainer.innerHTML = wishlistCourses.map(course => `
        <div class="col-lg-6 mb-4">
            <div class="wishlist-item">
                <div class="row">
                    <div class="col-md-4">
                        <img src="${course.image}" alt="${course.title}" class="img-fluid rounded">
                    </div>
                    <div class="col-md-8">
                        <h5>${course.title}</h5>
                        <p class="text-muted mb-2">by ${course.instructor}</p>
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="course-price">$${course.price}</span>
                            <div>
                                <button class="btn btn-primary btn-sm me-2" onclick="enrollInCourse(${course.id})">Enroll Now</button>
                                <button class="btn btn-outline-danger btn-sm" onclick="addToWishlist(${course.id})">Remove</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `).join('');
}

function showWishlist() {
    // This would typically open a modal or navigate to wishlist page
    showNotification('Wishlist feature coming soon!', 'info');
}

// Utility Functions
function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
    notification.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    document.body.appendChild(notification);
    
    // Auto-remove after 5 seconds
    setTimeout(() => {
        if (notification.parentNode) {
            notification.remove();
        }
    }, 5000);
}

// Load saved theme on page load
document.addEventListener('DOMContentLoaded', loadSavedTheme);

