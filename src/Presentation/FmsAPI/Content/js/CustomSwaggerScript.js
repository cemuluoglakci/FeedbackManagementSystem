(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');;
    document.head.removeChild(link);
    link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    document.head.removeChild(link);
    var title = document.querySelector("title") || document.createElement('title');
    document.head.removeChild(title)
    link = document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = '../Content/logo.png';
    document.getElementsByTagName('head')[0].appendChild(link);

    title = document.createElement('title');
    const titleContent = document.createTextNode("FMS API Docs");
    title.appendChild(titleContent);
    document.getElementsByTagName('head')[0].appendChild(title);
})();