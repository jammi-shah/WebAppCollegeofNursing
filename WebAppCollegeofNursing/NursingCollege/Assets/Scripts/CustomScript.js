function getResultByYear() {
    
    event.preventDefault();
    var year = $('#ddlYear').val();
    var divMessage = $('#divMessage');
    var resultDiv = $('#resultDiv');
    divMessage.hide();
    if (year == "") {
        resultDiv.empty();
        divMessage.show();
        divMessage.text('Please select year');
        return false;
    }
    
    $.ajax({
        url: '/Student/Result/GetResultByYear',
        method: 'get',
        data: { year: year },
        success: function (res) {
            resultDiv.empty();
            if (res[0] == undefined) {
                resultDiv.html(`
                    <br /> <br /> <br />
                    <div class="alert alert-danger">
                        Result not available
                    </div>
                `);
                return false;
            }
            
            $(res).each(function (i, result) {
                resultDiv.append(`
                <div class="col-md-4">
                
                <table style="border:1px solid #dddddd" class="table table-responsive table-hover table-condensed">
                    <tr><td colspan="2"><h3 class="text-center">${result.Subject}</h3></td></tr>
                    <tr>
                        <td>Paper1</td>
                        <td>${result.Paper1}</td>
                    </tr>
                    
                    <tr>
                        <td>Paper2</td>
                        <td>${result.Paper2}</td>
                    </tr>
                    <tr>
                        <td>Paper3</td>
                        <td>${result.Paper3}</td>
                    </tr>
                    <tr>
                        <td>ModelPaper</td>
                        <td>${result.ModelPaper}</td>
                    </tr>
                    <tr>
                        <td>PPT</td>
                        <td>${result.PPT}</td>
                    </tr>
                    <tr>
                        <td>Assignment</td>
                        <td>${result.Assignment}</td>
                    </tr>
                    <tr>
                        <td>ClassPresentation</td>
                        <td>${result.ClassPresentation}</td>
                    </tr>
                    <tr>
                        <td>Attendence</td>
                        <td>${result.Attendence}</td>
                    </tr>
                </table
                </div>
                 `)
            });
           
        },
        error: function (xhr) {
            alert('hi');
        }

    });
}