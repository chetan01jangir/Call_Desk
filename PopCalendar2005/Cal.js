/**
 * @fileoverview Loads generic modules required for all widgets.
 */

if (typeof Cal == 'undefined') {
  /**
   * Namespace definition.
   * @constructor
   */
  Cal = function() {};
}

/**
 * Returns path from the last loaded script element. Splits src attribute value
 * and returns path without js file name.
 *
 * <p><strong>
 * Note: This function should not be called from another function. It must be
 * invoked during page load to determine path to js file from which it is called
 * correctly.
 * </strong></p>
 *
 * @private
 * @return Path to the script, e.g. '../src/' or '' if path is not found
 * @type string
 */
Cal.getPath = function() {
  // Get last script element
  var objContainer = document.body;
  if (!objContainer) {
    objContainer = document.getElementsByTagName('head')[0];
    if (!objContainer) {
      objContainer = document;
    }
  }
  var objScript = objContainer.lastChild;
  // Get path
  var strSrc = objScript.getAttribute('src');
  if (!strSrc) {
    return '';
  }
  var arrTokens = strSrc.split('/');
  // Remove last token
  arrTokens = arrTokens.slice(0, -1);
  if (!arrTokens.length) {
    return '';
  }
  return arrTokens.join('/') + '/';
};

/**
 * Simply writes script tag to the document.
 *
 * @private
 * @param {string} strSrc Src attribute value of the script element
 */
Cal.include = function(strSrc) {
  document.write('<s' + 'cript type="text/javascript" src="' + strSrc +
   '"></s' + 'cript>');
};

/**
 * Path to main Cal script.
 * @private
 */
Cal.zapatecPath = Cal.getPath();

// Include required scripts
Cal.include(Cal.zapatecPath + 'utils.js');
Cal.include(Cal.zapatecPath + 'transport.js');
Cal.include(Cal.zapatecPath + 'zpwidget.js');

// Replace Cal.include with more complex function from transport library
if (Cal.Transport && Cal.Transport.include) {
  Cal.include = Cal.Transport.include;
}
