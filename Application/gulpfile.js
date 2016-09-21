/// <binding ProjectOpened='watch-global' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
(function () {
    /* List of necessary gulp plugins */
    var gulp = require('gulp');
    var gulpFilter = require('gulp-filter');
    var bower = require('gulp-bower');
    var angularFileSort = require('gulp-angular-filesort');
    var inject = require('gulp-inject');
    var concat = require('gulp-concat');
    var minify = require('gulp-minify-css');
    var uglify = require('gulp-uglify');
    var concatCss = require('gulp-concat-css');
    var less = require('gulp-less');
    var jshint = require('gulp-jshint');
    var eventStream = require('event-stream');
    var install = require('gulp-install');
    var rename = require('gulp-rename');
    var bowerFiles = require('main-bower-files');

    /* Path constants */
    var ngPath = 'app';
    var ngAllPath = ngPath + '/**/*.js';
    var ngLandingPath = ngPath + '/LandingPage/**/*.js';
    var ngMainPath = ngPath + '/mainPage/**/*.js';
    var dist = 'dist';

    //JavaScript third-party libraries
    var jsAngularPath = 'bower_components/angular/angular.js';
    var jsAngularResourcePath = 'bower_components/angular-resource/angular-resource.js';
    var jsAngularRoutePath = 'bower_components/angular-route/angular-route.js';
    var jsAngularLocalStoragePath =
        'bower_components/angular-local-storage/dist/angular-local-storage.js';
    var jsAngularAnimatePath = 'bower_components/angular-animate/angular-animate.js';
    var jsAngularUIRouterPath =
        'bower_components/angular-ui-router/release/angular-ui-router.js';
    var jsJQueryPath = 'bower_components/jquery/dist/jquery.js'
    var jsJqueryEasingPath = 'bower_components/jquery-easing/jquery.easing.js';
    var jsScrollingNavPath = 'bower_components/scrolling-nav.js'; //manual download
    var jsBootstrapPath = 'bower_components/bootsrap/dist/js/bootstrap.js';
    var jsBootstrapTlpsPath = 'bower_components/ui-bootstrap-tpls-0.14.3.js'; //manual download

    var jsLibsPath = [jsAngularPath, jsAngularResourcePath, jsAngularRoutePath,
    jsAngularLocalStoragePath, jsAngularAnimatePath, jsAngularUIRouterPath, jsJQueryPath,
    jsJqueryEasingPath, jsScrollingNavPath, jsBootstrapPath, jsBootstrapTlpsPath];

  
    /* Functions and tasks */
    gulp.task('run-all', ['bower-js-concat-min', 'bower-css-concat-min',
         'landing-page-js-concat-min', 'landing-page-css-concat-min',
         'main-page-css-concat-min', 'main-page-js-concat-min',
         'additional-js-concat-min'
    ]);

    /* Install Bower packages */
    gulp.task('bower-install-packages', function () {
        return bower('./bower_components');
    });
    /* Insert script references to html */
    gulp.task('insert-script-refs', function () {
        var target = gulp.src('index.html');
        var sources = gulp.src(ngAllPath);
        return target.pipe(inject(sources.pipe(angularFileSort())))
            .pipe(gulp.dest(''));
    });

    /*Bower Components: Concatenate and copy .js files */
    gulp.task('bower-js-concat-min', function () {
      /*  var jsFilter = gulpFilter('*.js');
        var jsCustomPath = ['bower_components/scrolling-nav.js',
                            'bower_components/ui-bootstrap-tpls-0.14.3.js'];
        var jsDist = dist + '/js';
        var baseStream = gulp.src(bowerFiles)
             .pipe(jsFilter);
        var customStream = gulp.src(jsCustomPath)
        return eventStream.merge(baseStream, customStream)
               .pipe(angularFileSort())
               .pipe(concat('bower-components.min.js'))
               .pipe(uglify())
               .pipe(gulp.dest(jsDist));*/
        var jsDist = dist + '/js';
        return gulp.src(jsLibsPath)
                .pipe(concat('bower-components.min.js'))
                .pipe(uglify())
                .pipe(gulp.dest(jsDist));
    });

    /*Bower Components: Concatenate and copy .css, .less files */
    gulp.task('bower-css-concat-min', function () {
        var cssDist = dist + '/css';
        var cssFilter = gulpFilter('*.css');
        var lessFilter = gulpFilter(['*.less', '!bootstrap.less']);
        var cssStream = gulp.src(bowerFiles())
            .pipe(cssFilter);
        var lessStream = gulp.src(bowerFiles())
            .pipe(lessFilter)
            .pipe(less());
        return eventStream.merge(cssStream,lessStream)
            .pipe(concat('bower-components.min.css'))
            .pipe(minify())
            .pipe(gulp.dest(cssDist));
    });

    /*Addiotional js:  Concatenate and copy .js files */
    gulp.task('additional-js-concat-min', function () {
        var ngDist = dist + '/js';
        var validators = 'app/validators/*.js';
        var constants = 'app/app.constants.js';
        var jsPath = [validators, constants];
        return gulp.src(jsPath)
            .pipe(angularFileSort())
            .pipe(concat('common.min.js'))
            .pipe(uglify())
            .pipe(gulp.dest(ngDist));
    });

    /*LandingPage: Concatenate and minify .js files */
    gulp.task('landing-page-js-concat-min', function () {
        var ngDist = dist + '/js';
        return gulp.src(ngLandingPath)
            .pipe(angularFileSort())
            .pipe(concat('landing-page.min.js'))
            .pipe(uglify())
            .pipe(gulp.dest(ngDist));
    });

    /*LandingPage: Concatenate and minify .less and .css files */
    gulp.task('landing-page-css-concat-min', function () {
        var cssDist = dist + '/css';
        var cssLandingPath = 'app/LandingPage/**/*.css';
        var lessLandingPath = 'app/LandingPage/**/*.less';
        return eventStream.merge(
                gulp.src(cssLandingPath)
                .pipe(concatCss('landing-page.css'))
                .pipe(minify()),
                gulp.src(lessLandingPath)
                .pipe(less())
                .pipe(concatCss('landing-page-less.css'))
                .pipe(minify())
            )
            .pipe(concat('landing-page.min.css'))
            .pipe(gulp.dest(cssDist));
    });

    /*MainPage: Concatenate and minify .js files */
    gulp.task('main-page-js-concat-min', function () {
        var ngDist = dist + '/js';
        return gulp.src(ngMainPath)
            .pipe(angularFileSort())
            .pipe(concat('main-page.min.js'))
            .pipe(uglify())
            .pipe(gulp.dest(ngDist));
    });

    /*MainPage: Concatenate and minify .less and .css files */
    gulp.task('main-page-css-concat-min', function () {
        var cssDist = dist + '/css';
        var cssMainPath = 'app/mainPage/**/*.css';
        var lessMainPath = 'app/mainPage/**/*.less';
        var cssStream = gulp.src(cssMainPath);
        var lessStream = gulp.src(lessMainPath)
             .pipe(less());
        return eventStream.merge(cssStream, lessStream)
            .pipe(concat('main-page.min.css'))
            .pipe(minify())
            .pipe(gulp.dest(cssDist));
    });

    //Watches
    /*Landing Page: Automate concatenation, minification
      and moving of js, less, css files*/
    gulp.task('landing-page-watch', function () {
        var landingPagePath = ngPath + 'LandingPage/**/*.{less,css,js}';
        gulp.watch(landingPagePath, [
            'landing-page-js-concat-min',
            'landing-page-css-concat-min'
        ]);
    });

    /*Main Page: Automate concatenation, minification
  and moving of js, less, css files*/
    gulp.task('main-page-watch', function () {
        var mainPagePath = ngPath + 'mainPage/**/*.{less,css,js}';
        gulp.watch(mainPagePath, [
            'main-page-js-concat-min',
            'main-page-css-concat-min'
        ]);
    });

    /*Additional files: Automate concatenation, minification of js files in app/ */
    gulp.task('additional-js-watch', function () {
        var filters = 'app/filters/*.js';
        var validators = 'app/validators/*.js';
        var constants = 'app/app.constants.js';
        var jsPath = [filters, validators, constants];
        gulp.watch(jsPath, ['additional-js-concat-min']);
    });

    /* Bower Components: Automate concatenation and copy js, css minified files */
    gulp.task('bower-components-watch', function () {
        var bowerComponentsPath = 'bower_components/**/*.{css,less,js}';
        gulp.watch(bowerComponentsPath, [
        'bower-js-concat-min',
        'bower-css-concat-min'
        ]);
    });

    /* Watch chain */
    gulp.task('watch-global', ['bower-components-watch', 'additional-js-watch',
        'landing-page-watch', 'main-page-watch']);

    /* Planned tasks for future:
    1. Minimizing and moving of images.
    2. Configure css and js reference injection.
    3. Configure html code blocks injection.
    */
})();
