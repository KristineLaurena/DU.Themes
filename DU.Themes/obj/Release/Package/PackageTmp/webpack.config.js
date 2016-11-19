/// <reference path="F:\Soft\DU.Themes\DU.Themes\Scripts/main.js" />
var webpack = require('webpack');

module.exports = {
  entry: "./Scripts/main.js",
  output: {
    filename: "./Scripts/bundle.js",
  },
  plugins: [
      new webpack.ProvidePlugin({
        $: "jquery/dist/jquery.js",
        jQuery: "jquery/dist/jquery.js",
        "window.jQuery": "jquery/dist/jquery.js"
      })
  ],
  resolve: {
    root: ['./bower_components']
  }
};