var map;
var autocomplete;


function fnCargaMapaCoincidencias(strValor) {

    var input = document.getElementById('inputUbicacionMapa');
    input = strValor;

    var autocompleteService = new google.maps.places.AutocompleteService();
    var request = { input: strValor };
    autocompleteService.getPlacePredictions(request, (predictionsArr, placesServiceStatus) => {
        console.log('getting place predictions :: predictionsArr = ', predictionsArr, '\n',
            'placesServiceStatus = ', placesServiceStatus);

        var placeRequest = { placeId: predictionsArr[0].place_id };
        var placeService = new google.maps.places.PlacesService(map);
        placeService.getDetails(placeRequest, (placeResult, placeServiceStatus) => {
            console.log('placeService :: placeResult = ', placeResult, '\n',
                'placeServiceStatus = ', placeServiceStatus);

            autocomplete.set("place", placeResult)

            //google.maps.event.trigger(autocomplete, 'place_changed');

        });
    });

}


function initMap() {
   if (objMapaConfiguracion == undefined) {

       //vista inicial del mapa
       objMapaConfiguracion = {
           lat: 27.1324452,
           lng: - 103.0649642,
           zoom: 6,
           disableDoubleClickZoom : true
        }

    }

    map = new google.maps.Map(document.getElementById('mapGoogle'), {
        center: {
            lat: objMapaConfiguracion.lat,
            lng: objMapaConfiguracion.lng
        },
        zoom: objMapaConfiguracion.zoom,
        disableDoubleClickZoom: objMapaConfiguracion.disableDoubleClickZoom
    });

    var input = document.getElementById('inputUbicacionMapa');

    autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    // Specify just the place data fields that you need.
    //autocomplete.setFields(['place_id', 'geometry', 'name']);

    autocomplete.setFields(
        ['address_components', 'geometry', 'icon', 'name']);

    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);


    var infowindow = new google.maps.InfoWindow();
    var infowindowContent = document.getElementById('infowindow-content');
    infowindow.setContent(infowindowContent);
    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, -29)
    });

    if (objMapaConfiguracion != undefined) {
        marker.setPosition(objMapaConfiguracion);
        marker.setVisible(true);
        infowindowContent.children['place-name'].textContent = fnNombreMarca();
        infowindowContent.children['place-address'].textContent = objMapaConfiguracion.direccionMarca;
        infowindow.open(map, marker);
    }

    //var infowindow = new google.maps.InfoWindow();
    //var infowindowContent = document.getElementById('infowindow-content');
    //infowindow.setContent(infowindowContent);
    //var marker = new google.maps.Marker({ map: map });

    map.addListener('dblclick', function (event) {
        var myLatLng = event.latLng;       
        fnAsignarPosicion(myLatLng);        
        marker.setPosition(myLatLng);        
        marker.setTitle("Oficina");
        infowindowContent.children['place-name'].textContent = fnNombreMarca();
        map.setZoom(17);
    })

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

    autocomplete.addListener('place_changed', function () {
        infowindow.close();

        var place = autocomplete.getPlace();

        if (!place.geometry) {
            return;
        }

        //if (place.geometry.viewport) {
        //    map.fitBounds(place.geometry.viewport);
        //} else {
        //    map.setCenter(place.geometry.location);
        //    map.setZoom(17);
        //}

        map.setCenter(place.geometry.location);
        map.setZoom(17);

        fnAsignarPosicion(place.geometry.location);

        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }

        //// Set the position of the marker using the place ID and location.
        //marker.setPlace({
        //    placeId: place.place_id,
        //    location: place.geometry.location
        //});

        //marker.setVisible(true);


        infowindowContent.children['place-icon'].src = place.icon;
        infowindowContent.children['place-name'].textContent = fnNombreMarca();
        infowindowContent.children['place-address'].textContent = address;
        infowindow.open(map, marker);

        //infowindowContent.children['place-name'].textContent = place.name;
        //infowindowContent.children['place-id'].textContent = place.place_id;
        //infowindowContent.children['place-address'].textContent =
        //    place.formatted_address;
        //infowindow.open(map, marker);

    });
 }