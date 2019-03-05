function initMap() {
    var map = new google.maps.Map(document.getElementById('mapGoogle'), {
        zoom: 7,
        center: { lat: 27.1324452, lng: - 103.0649642 }
    });

    var ctaLayer = new google.maps.KmlLayer({
        url: '/Content/kml/CapaMunicipios.kml',
        map: map
    });

    setMarkers(map);
}

// Data for the markers consisting of a name, a LatLng and a zIndex for the
// order in which these markers should display on top of each other.

var oficinas = [];
$(".regOficina").each(function () {
    var oid = parseInt($(this).attr('oid'));
    var id = parseInt($(this).attr('id'));
    var mpio = $(this).attr('mpio');  
    var latitud = parseFloat($(this).attr("lat"));
    var longitud = parseFloat($(this).attr("lng"));
    var tipoMarca = parseInt($(this).attr("tipo"));
    var urlMarca;
    var titulo;
    if (tipoMarca == 1) {
        urlMarca = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
        titulo = "Oficialía No." + id + " | " + mpio;
    } else {
        urlMarca = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';
        titulo = "Hospital-módulo No." + id + " | " + mpio;
    }

    oficinas.push([titulo, latitud, longitud, oid, urlMarca]); 
}); 

function setMarkers(map) {
    // Adds markers to the map.

    // Marker sizes are expressed as a Size of X,Y where the origin of the image
    // (0,0) is located in the top left of the image.

    // Origins, anchor positions and coordinates of the marker increase in the X
    // direction to the right and in the Y direction down.
    var image = {
        url: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png',
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(32, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32)
    };
    // Shapes define the clickable region of the icon. The type defines an HTML
    // <area> element 'poly' which traces out a polygon as a series of X,Y points.
    // The final coordinate closes the poly by connecting to the first coordinate.
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };


    for (var i = 0; i < oficinas.length; i++) {
        var oficina = oficinas[i];

        var iconMarca = image;
        iconMarca.url = oficina[4];

        var marker = new google.maps.Marker({
            position: { lat: oficina[1], lng: oficina[2] },
            map: map,
            icon: iconMarca,
            shape: shape,
            title: oficina[0],
            zIndex: oficina[3]
        });
    }
}
