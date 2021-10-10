


var map = L.map('mapid').setView([35.9375, 14.3754], 13);
var map2 = L.map('mapid2').setView([35.9375, 14.3754], 13);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map2);

var geocodeService = L.esri.Geocoding.geocodeService({
    apikey: "AAPK042faa12737b42b0b4286505141f9cc1wOzDPSo_t3pApIyiQu_1ZWwol0MAw3EZRtKU5oyF72MB0HLt6MTYHGdCYgUFFRlo" // replace with your api key - https://developers.arcgis.com
});


map.on('click', function (e) {
    geocodeService.reverse().latlng(e.latlng).run(function (error, result) {
        if (error) {
            return;
        }

        L.marker(result.latlng).addTo(map).bindPopup(result.address.Match_addr).openPopup();
        document.getElementById("residingAdress").value = result.address.Match_addr;
       
    });

});


map2.on('click', function (e) {
    geocodeService.reverse().latlng(e.latlng).run(function (error, result) {
        if (error) {
            return;
        }

        L.marker(result.latlng).addTo(map2).bindPopup(result.address.Match_addr).openPopup();
        document.getElementById("destinationAddress").value = result.address.Match_addr;

    });

});
