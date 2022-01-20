import * as React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import PropTypes from 'prop-types';
import { Tab, Tabs, Typography } from '@mui/material';
import Navbar from '../Navbar/Navbar';
import SuggestTable from '../SuggestTable';

const mdTheme = createTheme();

function allyProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

function TabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box sx={{ p: 3 }}>
                    <Typography>{children}</Typography>
                </Box>
            )}
        </div>
    );
}

TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.any.isRequired,
};

export default function SuggestContainer(props) {
    const title = props.title || 'Suggest';

    const [value, setValue] = React.useState('');

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };



    return (
        <ThemeProvider theme={mdTheme}>
            <Box sx={{ display: 'flex' }}>

                <Navbar title={title} />

                <Box
                    component="main"
                    sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === 'light'
                                ? theme.palette.grey[100]
                                : theme.palette.grey[900],
                        flexGrow: 1,
                        height: '100vh',
                        overflow: 'auto',
                    }}
                >
                    <Toolbar />
                    <Box sx={{ width: '100%' }}>
                        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                            <Tabs onChange={handleChange}
                                value={value}
                                centered>
                                <Tab label='Google' {...allyProps(0)} />
                                <Tab label='Bing' {...allyProps(1)} />
                                <Tab label='DuckDuckGo' {...allyProps(2)} />
                            </Tabs>
                            <TabPanel
                                value={value} index={0}>
                                
                                <SuggestTable rows={null} />

                            </TabPanel>
                            <TabPanel
                                value={value} index={1}>
                                Bing
                            </TabPanel>
                            <TabPanel
                                value={value} index={2}>
                                DuckDuckGo
                            </TabPanel>
                        </Box>
                    </Box>

                </Box>
            </Box>
        </ThemeProvider>
    );
}