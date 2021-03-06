module.exports = {
	less: {
        files: [
            {'app/styles/app.css': ['app/less/app.less']},
            {'app/styles/material-design-icons.css': ['app/less/md.icon.less']}
        ],
        options: {
          compile: true
        }
    },
    angular: {
        files: {
            'angular/styles/app.min.css': [
                'libs/jquery/bootstrap/dist/css/bootstrap.css',
                'app/styles/font.css',
                'app/styles/app.css'
            ]
        },
        options: {
            compress: true
        }
    },
    html: {
        files: {
            'html/styles/app.min.css': [
                'app/styles/bootstrap.css',
                'app/styles/loading-bar.css',
                'app/styles/font.css',
                'app/styles/app.css'
            ]
        },
        options: {
            compress: true
        }
    }
}
