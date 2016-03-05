$(function(){
   //defaults
   $.fn.editable.defaults.url = '/post';
   $.fn.editable.defaults.placement = 'right'; 

   //primary applicant
    $('#broker-name').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#broker-email').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#area').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    var areas = [];
    $.each({"BD": "Bangladesh", 
            "BE": "Belgium", 
            "BF": "Burkina Faso", 
            "BG": "Bulgaria", 
            "BA": "Bosnia and Herzegovina", 
            "BB": "Barbados", "WF": "Wallis and Futuna", 
            "BL": "Saint Bartelemey", 
            "BM": "Bermuda", 
            "BN": "Brunei Darussalam", 
            "BO": "Bolivia", 
            "BH": "Bahrain", 
            "BI": "Burundi", 
            "BJ": "Benin", 
            "BT": "Bhutan", 
            "JM": "Jamaica", 
            "BV": "Bouvet Island", 
            "BW": "Botswana", 
            "WS": "Samoa", 
            "BR": "Brazil", 
            "BS": "Bahamas", 
            "JE": "Jersey"}, 
        function(k, v) {
            areas.push({id: k, text: v});
    }); 
    $('#assigned-broker').editable({
        type:'select2',
        source: areas,
        select2: {
            width: 200,
            placeholder: 'Select Area of Interest',
            allowClear: true
        } 
    });      

    

    $('#state').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    //approval-extended  (yes or no)
    $('#approval-extended').editable({
        type:'select',
        prepend: "please selected",
        source: [
            {value: 1, text: 'Yes'},
            {value: 2, text: 'No'}
        ] 
    });
    $('#previously-applied').editable({
        type:'select',
        prepend: "please selected",
        source: [
            {value: 1, text: 'Yes'},
            {value: 2, text: 'No'}
        ] 
    });
    
    $('#rent-amount').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#account-name').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#movein-date').editable({
        placement: 'right',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#contact-owner').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    
    //Applicant(s) Info1
    // $('#applicant-name').editable({
    //     validate: function(value) {
    //        if($.trim(value) == '') return 'This field is required';
    //     }
    // });
    $('#applicant-name').editable({
        url:'post',
        type:'address',
        value: {
            city: "Nevada", 
            street: "squarejet", 
            building: "10"
        },
        validate: function(value) {
            if(value.city == '') return 'city is required!'; 
        },
        display: function(value) {
            if(!value) {
                $(this).empty();
                return; 
            }
            var html = '<b>' + $('<div>').text(value.city).html() + '</b>, ' + $('<div>').text(value.street).html() + ' st., bld. ' + $('<div>').text(value.building).html();
            $(this).html(html); 
        }         
    }); 

    $('#applicant-email').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#age').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#phone').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#dob').editable({
      placement: 'right',
      type:'combodate',
      viewformat:'DD/MM/YYYY',
      template:'D / MMM / YYYY'
    });

    $('#ssn').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#monthly-pay').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#company').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#position').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#employer-cname').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#employer-cphone').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#job-startdate').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#current-address').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#landlord-name').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#current-rent').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#landlord-phone').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#lease-startdate').editable({
        placement: 'right',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });

    //Applicant(s) Info2
    $('#applicant-name2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#applicant-email2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#age2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#phone2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#dob2').editable({
        placement: 'right',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#ssn2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#monthly-pay2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#company2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#position2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#employer-cname2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#employer-cphone2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#job-startdate2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#current-address2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#landlord-name2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#current-rent2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#landlord-phone2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#lease-startdate2').editable({
        placement: 'right',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });

    //Additional Occupants
    $('#additional-name').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#additional-dob').editable({
        placement: 'top',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#additional-email').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#additional-name2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#additional-dob2').editable({
        placement: 'top',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#additional-email2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });

    //Additional Information
    $('#pets').editable({
         type:'select',
        prepend: "please selected",
        source: [
            {value: 1, text: 'Yes'},
            {value: 2, text: 'No'}
        ]
    });
    $('#breeds').editable({
        type:'select',
        prepend: "please selected",
        source: [
            {value: 1, text: 'Yes'},
            {value: 2, text: 'No'}
        ] 
    });
    $('#qua-pets').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#breed1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#weight1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#breed2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#weight2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#description').editable({
        type:'textarea',
        url: '/post',
        rows: 3
    });


    //Analysis income info
    $('#avg-salary1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-salary2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-salary3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-comission1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-comission2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-comission3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-income1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-income2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#avg-income3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#monthly-debt1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#monthly-debt2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#monthly-debt3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#verified1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#verified2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#verified3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });

    //table2
    $('#fico1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#fico2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#fico3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#jobyear1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#jobyear2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#jobyear3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#credit1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#credit2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#credit3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#total-credit1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#total-credit2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#total-credit3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
	$('#utilisation1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#utilisation2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#utilisation3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#bankrupt1').editable({
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#bankrupt2').editable({
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#bankrupt3').editable({
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#foreclosure1').editable({
        placement:'left',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#foreclosure2').editable({
        placement:'left',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });
    $('#foreclosure3').editable({
        placement:'left',
        type:'combodate',
        viewformat:'DD/MM/YYYY',
        template:'D / MMM / YYYY'
    });


    //Bank Account Info
    $('#bank-balance1').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#bank-balance2').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
    $('#bank-balance3').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });
     $('#other-balance').editable({
        validate: function(value) {
           if($.trim(value) == '') return 'This field is required';
        }
    });


   
});