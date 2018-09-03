$(document).ready(function () {
    $('#calendar').fullCalendar({
        selectable: true,
        header:
            {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
        buttonText: {
            today: 'Bugün',
            month: 'Ay',
            week: 'Hafta',
            day: 'Gün'
        },
        select: function (startDate, endDate) {
            var _start_time = moment(startDate).format('MMMM Do YYYY h:mm a');
            var _end_time = moment(endDate).format('MMMM Do YYYY h:mm a');
            $.ajax
                ({
                    url: '/Calendar/CreateAppointmentAll',
                    type: 'POST',
                    datatype: 'application/json',
                    contentType: 'application/json',
                    data: JSON.stringify({
                        startDate: +_start_time,
                        endDate: +_end_time
                    }),
                    success: function () {
                        window.location.href = 'Calendar/Create/0';
                    }
                });
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
                                description: data.appointment_before_note,
                                start: moment(data.appointment_start_date).format('YYYY-MM-DD'),
                                end: moment(data.appointment_end_date).format('YYYY-MM-DD'),
                                backgroundColor: data.appointment_color,
                                borderColor: "#fc0101"
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