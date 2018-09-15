$(document).ready(function () {
    $("#calendar").fullCalendar('destroy');
    $('#calendar').fullCalendar({
        eventClick: function (calEvent, jsEvent, view) {
            window.location.href = 'Calendar/Create/' + calEvent.id;
        },
        selectable: true,
        header:
            {
                left: 'prev,next, today',
                center: 'title',
                right: 'agendaDay,agendaWeek,month'
            },
        buttonText: {
            today: 'Bugün',
            month: 'Ay',
            week: 'Hafta',
            day: 'Gün'
        },
        select: function (startDate, endDate) {
            var today = new Date().getTime();
            var start = Date.parse(startDate);
            startDate = moment(startDate).format('MMMM Do YYYY h:mm a');
            endDate = moment(endDate).format('MMMM Do YYYY h:mm a');
            if (today > start) {
                alertify.confirm("Uyarı","Eski bir tarih seçtiniz devam edecek misiniz?",
                    function () {
                        $.ajax
                            ({
                                url: '/Calendar/CreateAppointmentAll',
                                type: 'POST',
                                datatype: 'application/json',
                                contentType: 'application/json',
                                data: JSON.stringify({
                                    startDate: startDate,
                                    endDate: endDate
                                }),
                                success: function () {
                                    window.location.href = '/Calendar/Create/0';
                                }
                            });
                    },
                    function () {
                        alertify.error('Cancel');
                    });
            }
            else {
                $.ajax
                    ({
                        url: '/Calendar/CreateAppointmentAll',
                        type: 'POST',
                        datatype: 'application/json',
                        contentType: 'application/json',
                        data: JSON.stringify({
                            startDate: startDate,
                            endDate: endDate
                        }),
                        success: function () {
                            window.location.href = '/Calendar/Create/0';
                        }
                    });
            }
        },
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Calendar/GetCalendarData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.appointment_before_note,
                                id: data.appointment_id,
                                description: data.appointment_before_note,
                                start: moment(data.appointment_start_date),
                                end: moment(data.appointment_end_date),
                                color: data.appointment_color_value,
                                evebackgroundColor: data.appointment_color_value
                            });
                    });
                    callback(events);
                }
            });
        },

        eventRender: function (event, element) {
            element.qtip(
                {
                    content: event.description
                });
        },

        editable: false
    });
});