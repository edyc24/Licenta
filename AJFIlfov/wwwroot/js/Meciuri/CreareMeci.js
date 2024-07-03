$(document).ready(function () {
    // Handle group selection
    $('select[name="IdGrupa"]').change(function () {
        var grupaId = $(this).val();
        if (grupaId) {
            $.getJSON('/Meciuri/GetTeamsByGrupa', { grupaId: grupaId }, function (data) {
                var echipeGazdaSelect = $('select[name="IdEchipaGazda"]');
                var echipeOaspeteSelect = $('select[name="IdEchipaOaspete"]');
                echipeGazdaSelect.empty();
                echipeOaspeteSelect.empty();

                echipeGazdaSelect.append($('<option>').text('-- Selectează Echipa Gazdă --').attr('value', ''));
                echipeOaspeteSelect.append($('<option>').text('-- Selectează Echipa Oaspete --').attr('value', ''));

                $.each(data, function (index, team) {
                    var option = $('<option>').text(team.nume).attr('value', team.id);
                    echipeGazdaSelect.append(option);
                    echipeOaspeteSelect.append(option.clone());
                });

            }).fail(function () {
                alert("Eroare la încărcarea echipelor. Vă rugăm să încercați din nou.");
            });
        } else {
            $('select[name="IdEchipaGazda"]').empty().append($('<option>').text('-- Selectează Echipa Gazdă --').attr('value', ''));
            $('select[name="IdEchipaOaspete"]').empty().append($('<option>').text('-- Selectează Echipa Oaspete --').attr('value', ''));
        }
    });

    function setupDistanceCalculation(refereeSelectName, distanceInfoId) {
        $('select[name="' + refereeSelectName + '"]').change(function () {
            var refereeId = $(this).val();
            var stadiumId = $('select[name="IdStadionLocalitate"]').val();
            if (refereeId && stadiumId) {
                calculateDistance(refereeId, stadiumId, distanceInfoId);
            } else {
                $('#' + distanceInfoId).text('');
            }
        });
    }

    setupDistanceCalculation('IdArbitru', 'distance-info-arbitru');
    setupDistanceCalculation('IdArbitruAsistent1', 'distance-info-arbitru1');
    setupDistanceCalculation('IdArbitruAsistent2', 'distance-info-arbitru2');
    setupDistanceCalculation('IdArbitruRezerva', 'distance-info-arbitru3');

    $('select[name="IdStadionLocalitate"]').change(function () {
        var stadiumId = $(this).val();
        setupDistanceCalculationOnStadiumChange(stadiumId);
    });

    function setupDistanceCalculationOnStadiumChange(stadiumId) {
        ['IdArbitru', 'IdArbitruAsistent1', 'IdArbitruAsistent2', 'IdArbitruRezerva'].forEach(function (refereeSelectName, index) {
            var refereeId = $('select[name="' + refereeSelectName + '"]').val();
            var distanceInfoId = 'distance-info-arbitru' + (index ? index : '');
            if (refereeId && stadiumId) {
                calculateDistance(refereeId, stadiumId, distanceInfoId);
            } else {
                $('#' + distanceInfoId).text('');
            }
        });
    }

    function calculateDistance(refereeId, stadiumId, distanceInfoId) {
        $.getJSON('/Meciuri/GetAddressData', { refereeId: refereeId, stadiumId: stadiumId }, function (data) {
            var refereeAddress = data.refereeAddress;
            var stadiumAddress = data.stadiumAddress;

            var service = new google.maps.DistanceMatrixService();
            service.getDistanceMatrix({
                origins: [refereeAddress],
                destinations: [stadiumAddress],
                travelMode: google.maps.TravelMode.DRIVING,
            }, function (response, status) {
                if (status === google.maps.DistanceMatrixStatus.OK) {
                    var results = response.rows[0].elements;
                    var distance = results[0].distance.text;
                    var duration = results[0].duration.text;

                    $('#' + distanceInfoId).text(`Distanță: ${distance}, Durată: ${duration}`);
                } else {
                    console.error('Eroare la calcularea distanței: ' + status);
                }
            });
        }).fail(function () {
            alert("Eroare la preluarea datelor de adresă. Vă rugăm să încercați din nou.");
        });
    }
});
