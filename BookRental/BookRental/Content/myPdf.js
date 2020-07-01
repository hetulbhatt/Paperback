const url = document.getElementById("path").value;

let pdfDoc = null,
	pageNum = 1,
	pageIsRendering = false,
	pageNumIsPending = null;

const scale = 1.5,
	canvas = document.querySelector('#pdf-render'),
	ctx = canvas.getContext('2d');

// Render the page
const renderPage = num => {
	pageIsRendering = true;

	// Get page
	pdfDoc.getPage(num).then(page => {
		// Set scale
		const viewport = page.getViewport({ scale });
		canvas.height = viewport.height;
		canvas.width = viewport.width;

		const renderCtx = {
			canvasContext: ctx,
			viewport
		};

		page.render(renderCtx).promise.then(() => {
			pageIsRendering = false;

			if (pageNumIsPending !== null) {
				renderPage(pageNumIsPending);
				pageNumIsPending = null;
			}
		});

		// Output current page
		document.querySelector('#page-num').textContent = num;
	});
};

// Check for pages rendering
const queueRenderPage = num => {
	if (pageIsRendering) {
		pageNumIsPending = num;
	} else {
		renderPage(num);
	}
};

// Show Prev Page
const showPrevPage = () => {
	if (pageNum <= 1) {
		return;
	}
	pageNum--;
	document.querySelector('#pageNumber').value = pageNum;
	queueRenderPage(pageNum);
};

// Show Next Page
const showNextPage = () => {
	if (pageNum >= pdfDoc.numPages) {
		return;
	}
	pageNum++;
	document.querySelector('#pageNumber').value = pageNum;
	queueRenderPage(pageNum);
};

// Show Button Page
const showButtonPage = () => {
	console.log(document.querySelector('#pageNumber').value);
	var thispage = parseInt(document.querySelector('#pageNumber').value);
	if (thispage >= pdfDoc.numPages+1) {
		document.querySelector('#pageNumber').value = pdfDoc.numPages;
		queueRenderPage(pdfDoc.numPages);
		return;
	}
	if (thispage < 1) {
		document.querySelector('#pageNumber').value = 1;
		queueRenderPage(1);
		return;
	}

	pageNum = thispage;
	console.log('Okay now: ' + pageNum);
	queueRenderPage(parseInt(pageNum));
};

// Get Document
pdfjsLib
	.getDocument(url)
	.promise.then(pdfDoc_ => {
		pdfDoc = pdfDoc_;

		document.querySelector('#page-count').textContent = pdfDoc.numPages;

		renderPage(pageNum);
		document.querySelector('#pageNumber').value = 1;
	})
	.catch(err => {
		// Display error
		const div = document.createElement('div');
		div.className = 'error';
		div.appendChild(document.createTextNode(err.message));
		document.querySelector('body').insertBefore(div, canvas);
		// Remove top bar
		document.querySelector('.top-bar').style.display = 'none';
	});

// Button Events
document.querySelector('#prev-page').addEventListener('click', showPrevPage);
document.querySelector('#next-page').addEventListener('click', showNextPage);
//document.querySelector('#button-page').addEventListener('click', showButtonPage);
document.querySelector('#pageNumber').addEventListener('change', showButtonPage);

