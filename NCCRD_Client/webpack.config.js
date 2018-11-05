const path = require('path')
const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const cwd = process.cwd()

const mode = 'production'

const CopyWebpackPlugin = require('copy-webpack-plugin')

const config = {
  plugins: [
    new CopyWebpackPlugin([{ from: 'source', to: 'dest' }])
  ]
}

/**
 * Config
 */
module.exports = {
  context: path.join(cwd, 'app'),
  mode,
  entry: {
    app: ["babel-polyfill", './js/index.jsx'],
    silentRenew: ["./silent_renew/silent_renew.js"],
    react: [
      'react',
      'react-dom',
      'react-router-dom',
      'react-router',
      'redux',
      'react-redux',
      'react-router-redux',
      'history'
    ],
  },

  output: {
    path: path.resolve('dist'),
    filename: 'bundle_[name].js'
  },

  module: {
    rules: [{
      test: /\.jsx?$/,
      use: ['babel-loader'],
      exclude: /node_modules/
    },
    {
      test: /\.(json)$/,
      use: [
        'json-loader'
      ]
    },
    {
      test: /\.css$/,
      use: [
        'style-loader',
        'css-loader'
      ]
    },
    {
      test: /\.less$/,
      use: [
        'style-loader',
        'css-loader',
        'less-loader'
      ]
    },
    {
      test: /\.scss$/,
      use: [
        'style-loader',
        'css-loader',
        'scss-loader'
      ]
    },
    {
      test: /\.(png|jpg|jpeg|svg|gif)$/,
      use: [
        'file-loader'
      ]
    },
    {
      test: /\.(woff|woff2|eot|ttf|otf)$/,
      use: [
        'file-loader'
      ]
    },
    ]
  },

  plugins: [
    new HtmlWebpackPlugin({
      template: './index.ejs',
    }),
    new HtmlWebpackPlugin({
      template: "./silent_renew/silent_renew.html",
      chunks: ["silentRenew"],
      filename: "silent_renew.html"
    }),
    new webpack.DefinePlugin({
      CONSTANTS: {
        PRODUCTION: mode === 'production'
      }
    }),
    new webpack.IgnorePlugin(/^(fs|ipc|cfg)$/),
    new CopyWebpackPlugin([
      {
        from: 'js/config/*.cfg',
        to: '[name].[ext]',
        toType: 'template'
      }
    ])
  ]
}