<% Response.StatusCode = 500; %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Internal Server Error :(</title>
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300">
    <style>
        ::-moz-selection {
            background: #b3d4fc;
            -webkit-text-shadow: none;
            text-shadow: none;
        }

        ::selection {
            background: #b3d4fc;
            -webkit-text-shadow: none;
            text-shadow: none;
        }

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            padding: 30px 10px;
            background: #f0f0f0;
            color: #737373;
            font-size: 20px;
            line-height: 1.4;
        }

        body {
            margin: 0 auto;
            padding: 30px 20px 50px;
            max-width: 500px;
            _width: 500px;
            border: 1px solid #b3b3b3;
            border-radius: 4px;
            background: #fcfcfc;
            box-shadow: 0 1px 10px #a7a7a7, inset 0 1px 0 #fff;
            font-size: 14px;
            font-family: "Open Sans", Helvetica, Arial, sans-serif;
        }

        h1 {
            margin: 0 10px;
            text-align: center;
            font-size: 50px;
        }

            h1 span {
                color: #bbb;
            }

        h3 {
            margin: 1.5em 0 0.5em;
        }

        p {
            margin: 1em 0;
        }

        ul {
            margin: 1em 0;
            padding: 0 0 0 40px;
        }

        .container {
            margin: 0 auto;
            max-width: 380px;
            _width: 380px;
        }

        /* google search */

        #goog-fixurl ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        #goog-fixurl form {
            margin: 0;
        }

        #goog-wm-qt,
        #goog-wm-sb {
            border: 1px solid #bbb;
            font-size: 16px;
            line-height: normal;
            vertical-align: top;
            color: #444;
            border-radius: 2px;
        }

        #goog-wm-qt {
            width: 220px;
            height: 20px;
            padding: 5px;
            margin: 5px 10px 0 0;
            box-shadow: inset 0 1px 1px #ccc;
        }

        #goog-wm-sb {
            display: inline-block;
            height: 32px;
            padding: 0 10px;
            margin: 5px 0 0;
            white-space: nowrap;
            cursor: pointer;
            background-color: #f5f5f5;
            background-image: -webkit-linear-gradient(rgba(255,255,255,0), #f1f1f1);
            background-image: -moz-linear-gradient(rgba(255,255,255,0), #f1f1f1);
            background-image: -ms-linear-gradient(rgba(255,255,255,0), #f1f1f1);
            background-image: -o-linear-gradient(rgba(255,255,255,0), #f1f1f1);
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            *overflow: visible;
            *display: inline;
            *zoom: 1;
        }

            #goog-wm-sb:hover,
            #goog-wm-sb:focus {
                border-color: #aaa;
                box-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);
                background-color: #f8f8f8;
            }

        #goog-wm-qt:hover,
        #goog-wm-qt:focus {
            border-color: #105cb6;
            outline: 0;
            color: #222;
        }

        input::-moz-focus-inner {
            padding: 0;
            border: 0;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Internal server error <span>:(</span></h1>
        <p>Oops! Something went terribly wrong. Please try again later.</p>
    </div>
</body>
</html>