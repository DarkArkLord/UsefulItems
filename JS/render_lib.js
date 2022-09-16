const HTMLTags = {
    Table: 'table',
    TableRow: 'tr',
    TableData: 'td',
    h2: 'h2',
    TextArea: 'textarea',
    Button: 'button',
    Div: 'div',
}

function render_V1(item) {
    if (item.element) {
        return item.element;
    }

    let element = document.createElement(item.tag);
    item.element = element;

    if (item.attributes) {
        for (let name in item.attributes) {
            let value = item.attributes[name];
            element.setAttribute(name, value);
        }
    }

    if (item.value) {
        element.innerHTML = item.value;
    }

    if (item.childs) {
        for (let child of item.childs) {
            if (!child.element) {
                render_V1(child);
            }
            element.append(child.element);
        }
    }

    return element;
}

export function render_V2(tag, attributes, ...childs) {
    if (tag instanceof Function) {
        return tag(attributes, ...childs);
    }

    let element = document.createElement(tag);

    if (attributes) {
        for (const name in attributes) {
            let value = attributes[name];
            element.setAttribute(name, value);
        }
    }

    for (const child of childs) {
        (function addChild(parent, child) {
            if (Array.isArray(child)) {
                for (const innerChild of child) {
                    addChild(parent, innerChild);
                }
            } else {
                parent.appendChild(
                    child instanceof HTMLElement
                    ? child
                    : document.createTextNode(child)
                );
            }
        })(element, child);
    }

    return element;
}

const SVGTags = {
    SVG: 'svg',
    Group: 'g',
    Line: 'line',
    Polyline: 'polyline',
    Text: 'text'
}

function renderSVG(item) {
    if (item.element) {
        return item.element;
    }
    let element = item.element = document.createElementNS('http://www.w3.org/2000/svg', item.tag);
    if (item.attributes) {
        for (let name in item.attributes) {
            let value = item.attributes[name];
            element.setAttributeNS(null, name, value);
        }
    }
    if (item.value) {
        element.innerHTML = item.value;
    }
    if (item.innerElement) {
        element.appendChild(item.innerElement);
    }
    if (item.childs) {
        for (let child of item.childs) {
            renderSVG(child);
            element.append(child.element);
        }
    }
    return element;
}