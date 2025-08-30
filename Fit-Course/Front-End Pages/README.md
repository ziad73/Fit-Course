# LearnHub - Online Learning Platform

A modern, responsive front-end for an online courses website similar to Udemy, built with Bootstrap 5 and vanilla JavaScript.

## 🚀 Features

### Core Functionality
- **User Authentication** - Mock login/signup system with LocalStorage persistence
- **Course Management** - Browse, search, and filter courses by category, level, and price
- **Enrollment System** - Enroll in courses and track progress
- **Wishlist Management** - Save courses for later
- **Progress Tracking** - Monitor learning progress with visual progress bars
- **Dark Mode** - Toggle between light and dark themes

### Pages & Components
- **Landing Page** - Hero section, featured courses, categories, and search
- **Course Listing** - Grid layout with advanced filters and sorting
- **Course Detail** - Comprehensive course information, syllabus, and enrollment
- **User Dashboard** - Personal learning progress, enrolled courses, and wishlist
- **Responsive Navigation** - Sticky navbar with user menu and search
- **Footer** - Links, social media, and newsletter signup

### Technical Features
- **Fully Responsive** - Mobile-first design that works on all devices
- **LocalStorage Integration** - Persistent data storage for user state
- **Modern UI/UX** - Smooth animations, hover effects, and interactive elements
- **Bootstrap 5** - Latest Bootstrap framework with custom CSS variables
- **Font Awesome Icons** - Professional iconography throughout the interface

## 🛠️ Technology Stack

- **HTML5** - Semantic markup and accessibility
- **CSS3** - Custom properties, animations, and responsive design
- **Bootstrap 5** - UI framework and components
- **Vanilla JavaScript** - ES6+ features and modern DOM manipulation
- **Font Awesome 6** - Icon library
- **LocalStorage API** - Client-side data persistence

## 📁 Project Structure

```
OnlineCoursesWebsite/
├── index.html              # Landing page
├── courses.html            # Course listing page
├── course-detail.html      # Individual course page
├── dashboard.html          # User dashboard
├── css/
│   └── style.css          # Custom styles and CSS variables
├── js/
│   └── app.js             # Main JavaScript application
├── images/                 # Image assets directory
└── README.md              # Project documentation
```

## 🚀 Getting Started

### Prerequisites
- Modern web browser (Chrome, Firefox, Safari, Edge)
- Local web server (optional, for development)

### Installation
1. Clone or download the project files
2. Open `index.html` in your web browser
3. Start exploring the platform!

### Development Setup
1. Navigate to the project directory
2. Start a local web server (recommended):
   ```bash
   # Using Python 3
   python -m http.server 8000
   
   # Using Node.js
   npx serve .
   
   # Using PHP
   php -S localhost:8000
   ```
3. Open `http://localhost:8000` in your browser

## 💡 Usage Guide

### For Users
1. **Browse Courses** - Visit the courses page to see all available courses
2. **Create Account** - Click "Login" and then "Sign up" to create an account
3. **Enroll in Courses** - Click "Enroll Now" on any course you're interested in
4. **Track Progress** - Visit your dashboard to see enrolled courses and progress
5. **Add to Wishlist** - Click the heart icon to save courses for later

### For Developers
- **Customization** - Modify CSS variables in `css/style.css` for easy theming
- **Adding Courses** - Edit the `loadSampleCourses()` function in `js/app.js`
- **New Features** - Extend the JavaScript functionality in `js/app.js`

## 🎨 Customization

### Colors and Themes
The application uses CSS custom properties for easy theming. Modify these variables in `css/style.css`:

```css
:root {
    --primary-color: #2563eb;
    --secondary-color: #64748b;
    --success-color: #10b981;
    --warning-color: #f59e0b;
    --danger-color: #ef4444;
    /* ... more variables */
}
```

### Dark Mode
Dark mode is automatically applied when the user toggles the theme. The system saves the user's preference in LocalStorage.

## 📱 Responsive Design

The application is built with a mobile-first approach and includes:
- Responsive grid layouts using Bootstrap's grid system
- Mobile-optimized navigation with collapsible menu
- Touch-friendly buttons and interactive elements
- Optimized typography for all screen sizes

## 🔧 Browser Support

- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

## 🚧 Future Enhancements

- **Backend Integration** - Real authentication and database
- **Video Player** - Integrated video streaming
- **Payment Processing** - Course purchase system
- **User Profiles** - Detailed user information and preferences
- **Course Reviews** - Rating and review system
- **Social Features** - User interactions and discussions
- **Mobile App** - Native mobile application

## 🤝 Contributing

1. Fork the project
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Author

Built with ❤️ for modern web development education.

## 🙏 Acknowledgments

- Bootstrap team for the excellent UI framework
- Font Awesome for the beautiful icons
- Unsplash for the sample images
- The open-source community for inspiration

---

**Note**: This is a front-end demonstration project. In a production environment, you would need to implement proper backend services, security measures, and data validation.

