/**
 * 自定义下拉框开始
 * @param {any} options
 * @returns
 */
function CustomSelect(options) {
    var select = document.createElement('div');
    select.className = 'custom-select';
  
    var selected = document.createElement('div');
    selected.className = 'select-selected';
    selected.textContent = options.defaultText || '请选择';
    select.appendChild(selected);
  
    var items = document.createElement('div');
    items.className = 'select-items';
  
    options.options.forEach(function(option) {
        var item = document.createElement('div');
        item.dataset.value = option.CategoryId
        item.textContent = option.CategoryName;
      items.appendChild(item);
    });
  
    select.appendChild(items);
  
    // 点击选项列表外的区域，收起选项列表
    document.addEventListener('click', function(e) {
      if (!select.contains(e.target)) {
        items.classList.remove('open');
        selected.classList.remove('dir')
        selected.classList.toggle('dir')
  
      }
    });
  
    // 点击选择区域，展开/收起选项列表
    selected.addEventListener('click', function() {
      items.classList.toggle('open');
      selected.classList.toggle('dir')
  //     setTimeout(()=>{
  // selected.style.transform=`rotate(${rotate+=180})`
  //     },500)
    });
  
    // 失去焦点时，收起选项列表
    selected.addEventListener('blur', function() {
      items.classList.remove('open');
      selected.classList.toggle('dir')
    });
  selected.addEventListener('focus',()=>{
      items.classList.add('open')
      selected.classList.toggle('dir')
  })
    // 选择选项
    Array.from(items.querySelectorAll('div')).forEach(function(item) {
      item.addEventListener('click', function() {
          selected.textContent = item.textContent;
          selected.dataset.value = item.getAttribute('data-value')
        items.classList.remove('open');
        selected.classList.toggle('dir')  
        if (options.onChange) {
          options.onChange(item);
        }
      });
    });  
    return select;
  }
fetch('/Main/CategoryManageSelect', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
   
})
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            throw new Error('请求失败');
        }
    })
    .then(data => {
        document.getElementById('custom-select').appendChild(new CustomSelect({
            options: data,
            defaultText: '选择比赛类别',
            onChange: function (selectedValue) {
              
            }
        }));
    })
    .catch(error => {
        console.error(error);
    });



/**
 * 自定义下拉框组件结束
 */



/**
 * 自定义时间选择组件开始
 * 
 */
class DatePicker {
    constructor(datePickerInputId, calendarClass) {
        this.datePickerInput = document.querySelector(`#${datePickerInputId}`);
        this.calendar = document.querySelector(`.${calendarClass}`);
        this.prevMonthButton = this.calendar.querySelector('#prev-month');
        this.prevYearButton = this.calendar.querySelector('#prev-year');
        this.nextMonthButton = this.calendar.querySelector('#next-month');
        this.nextYearButton = this.calendar.querySelector('#next-year');
        this.currentDateSpan = this.calendar.querySelector('.current-date');
        this.calendarTable = this.calendar.querySelector('table');

        this.currentYear = null;
        this.currentMonth = null;
        this.today = new Date();

        this.initialize();
    }

    initialize() {
        this.datePickerInput.readOnly = true;
        this.addEventListeners();
        this.generateCalendar();
    }

    addEventListeners() {
        document.addEventListener('click', (e) => {
            if (e.target == this.calendar || this.datePickerInput.contains(e.target)) {
                this.calendar.classList.toggle('show');
            } else {
                this.calendar.classList.remove('show');
            }
        });

        this.calendar.addEventListener('click', (event) => {
            if (event.target.tagName === 'TD') {
                const selectedDate = event.target.dataset.date;
                this.datePickerInput.value = selectedDate;
                this.calendar.classList.remove('show');
            }
        });

        this.prevMonthButton.addEventListener('click', (em) => {
            em.stopPropagation();
            if (this.currentMonth === 0) {
                this.currentYear--;
                this.currentMonth = 11;
            } else {
                this.currentMonth--;
            }
            this.generateCalendar();
        });

        this.nextMonthButton.addEventListener('click', (em) => {
            em.stopPropagation();
            if (this.currentMonth === 11) {
                this.currentYear++;
                this.currentMonth = 0;
            } else {
                this.currentMonth++;
            }
            this.generateCalendar();
        });

        this.prevYearButton.addEventListener('click', (em) => {
            em.stopPropagation();
            this.currentYear--;
            this.generateCalendar();
        });

        this.nextYearButton.addEventListener('click', (em) => {
            em.stopPropagation();
            this.currentYear++;
            this.generateCalendar();
        });
    }

    generateCalendar() {
        const currentDate = new Date(this.currentYear || this.today.getFullYear(), this.currentMonth || this.today.getMonth());
        this.currentYear = currentDate.getFullYear();
        this.currentMonth = currentDate.getMonth();

        const daysInMonth = new Date(this.currentYear, this.currentMonth + 1, 0).getDate();
        const firstDay = new Date(this.currentYear, this.currentMonth, 1).getDay() == 0 ? 6 : new Date(this.currentYear, this.currentMonth, 1).getDay() - 1;

        this.currentDateSpan.textContent = `${this.currentYear}年${this.currentMonth + 1}月`;

        const calendarBody = document.createElement('tbody');

        const weekRow = document.createElement('tr');
        const weekdays = ['一', '二', '三', '四', '五', '六', '日'];
        for (let i = 0; i < weekdays.length; i++) {
            const weekdayCell = document.createElement('th');
            weekdayCell.textContent = `周${weekdays[i]}`;
            weekRow.appendChild(weekdayCell);
        }
        calendarBody.appendChild(weekRow);

        let dayCount = 1;
        for (let i = 0; i < 6; i++) {
            const weekRow = document.createElement('tr');
            for (let j = 0; j < 7; j++) {
                if (i === 0 && j < firstDay) {
                    const lastMonthLastDay = new Date(this.currentYear, this.currentMonth, 0);
                    const lastYear = lastMonthLastDay.getFullYear();
                    const lastMonth = lastMonthLastDay.getMonth() + 1;
                    const lastDay = lastMonthLastDay.getDate() + 1;

                    for (let m = firstDay; m > 0; m--) {
                        j++;
                        const day = lastDay - m;
                        const emptyCell = document.createElement('td');
                        emptyCell.dataset.date = `${lastYear}-${lastMonth}-${day}`;
                        emptyCell.textContent = day;
                        emptyCell.classList.add('noCurrentdate');
                        if (j == 7 || j == 6) {
                            emptyCell.classList.add('noCurrentweek');
                        }
                        weekRow.appendChild(emptyCell);
                    }
                    j--;
                } else if (dayCount > daysInMonth) {
                    if (new Date(this.currentYear, this.currentMonth + 1, 0).getDay() !== 0) {
                        const nextDate = new Date(this.currentYear, this.currentMonth + 1, 1);
                        const nextYear = nextDate.getFullYear();
                        const nextMonth = nextDate.getMonth() + 1;
                        const nextDay = nextDate.getDate();
                        const count = 7 - j;

                        for (let m = 0; m < count; m++) {
                            j++;
                            const nextTD = document.createElement('td');
                            nextTD.textContent = nextDay + m;
                            nextTD.dataset.date = `${nextYear}-${nextMonth}-${nextDay + m}`;
                            if (j === 6 || j === 7) {
                                nextTD.classList.add('noCurrentweek');
                            }
                            nextTD.classList.add('noCurrentdate');
                            weekRow.appendChild(nextTD);
                        }
                    }
                    i = 5;
                    break;
                } else {
                    const dateCell = document.createElement('td');
                    dateCell.textContent = dayCount;

                    if (j === 5 || j === 6) {
                        dateCell.classList.add('weekend');
                    }

                    const currentDay = new Date(this.currentYear, this.currentMonth, dayCount);
                    const weekdayIndex = currentDay.getDay();
                    dateCell.dataset.date = `${this.currentYear}-${this.currentMonth + 1}-${dayCount}`;

                    if (dateCell.dataset.date == `${this.today.getFullYear()}-${this.today.getMonth() + 1}-${this.today.getDate()}`) {
                        dateCell.classList.add('selected');
                    }

                    weekRow.appendChild(dateCell);
                    dayCount++;
                }
            }
            calendarBody.appendChild(weekRow);
        }

        this.calendarTable.innerHTML = '';
        this.calendarTable.appendChild(calendarBody);
    }
}

const datePicker = new DatePicker('datepicker', 'calendar');

/**
 * 自定义时间选择组件结束
 */


