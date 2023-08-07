
new Chartist.Line('#views-graphic', {
	
  labels: ['Mon', 'Tue', 'Wed', 'Thur', 'Fri','Sat','Sun'],
  series: [
  [5,9,7,8,6,4,8]
  ]
}, {
  low: 0,
  showArea: true,
  fullWidth: true,
  distributeSeries: true,
	plugins: [
		Chartist.plugins.tooltip()
	]
});